// <copyright file="Browser.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium;

using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Vibium.Driver;

/// <summary>
/// Contains methods for interacting with a browser.
/// </summary>
public class Browser
{
    private const string DefaultClickerFileName = "clicker";
    private readonly ClickerOptions clickerOptions = new();
    private Process? clickerProcess;

    /// <summary>
    /// Gets or sets the port on which the Vibium proxy server should listen for communication.
    /// </summary>
    public int Port { get; set; } = 0;

    /// <summary>
    /// Gets or sets a value indicating the time to wait for an initial connection before timing out.
    /// </summary>
    public TimeSpan InitializationTimeout { get; set; } = TimeSpan.FromSeconds(20);

    /// <summary>
    /// Gets a value indicating whether the service is running.
    /// </summary>
    public bool IsRunning => this.clickerProcess is not null && !this.clickerProcess.HasExited;

    /// <summary>
    /// Launches the Vibium proxy server and the browser.
    /// </summary>
    /// <param name="options">The <see cref="LaunchOptions"/> object containing the options for launching the browser.</param>
    /// <returns>The <see cref="Vibe"/> object to drive the page.</returns>
    public async Task<Vibe> LaunchAsync(LaunchOptions options)
    {
        this.clickerProcess = new Process();
        this.clickerProcess.StartInfo.FileName = this.GetClickerPath(options);

        List<string> args = ["serve"];
        if (options.Port > 0)
        {
            args.Add("--port");
            args.Add(options.Port.ToString());
        }

        if (options.Headless)
        {
            args.Add("--headless");
        }

        this.clickerProcess.StartInfo.Arguments = string.Join(" ", args);
        this.clickerProcess.StartInfo.UseShellExecute = false;
        this.clickerProcess.StartInfo.RedirectStandardOutput = true;
        this.clickerProcess.StartInfo.RedirectStandardError = true;
        this.clickerProcess.StartInfo.CreateNoWindow = true;
        this.clickerProcess.ErrorDataReceived += this.ReadStandardError;
        this.clickerProcess.OutputDataReceived += this.ReadStandardOutput;
        this.clickerProcess.Start();
        this.clickerProcess.BeginOutputReadLine();
        this.clickerProcess.BeginErrorReadLine();
        bool launcherAvailable = await this.WaitForInitializationAsync().ConfigureAwait(false);

        VibiumDriver driver = new();
        await driver.StartAsync($"ws://localhost:{this.Port}");

        return new Vibe(driver, this);
    }

    /// <summary>
    /// Quits the Vibium proxy server.
    /// </summary>
    /// <returns>An empty result.</returns>
    public Task QuitAsync()
    {
        // TODO: Find a way to close the browser more gracefully.
        if (this.clickerProcess is not null)
        {
            if (!this.clickerProcess.HasExited)
            {
                this.clickerProcess.Kill();
            }

            this.clickerProcess = null;
        }

        return Task.CompletedTask;
    }

    [DllImport("libc", SetLastError = true)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "P/Invoke method, case is correct for platform-specific entry.")]
    private static extern int chmod(string path, int mode);

    /// <summary>
    /// Asynchronously waits for the initialization of the browser launcher.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    private async Task<bool> WaitForInitializationAsync()
    {
        bool isInitialized = false;
        DateTime timeout = DateTime.Now.Add(this.InitializationTimeout);
        while (!isInitialized && DateTime.Now < timeout)
        {
            // If the driver service process has exited, we can exit early.
            if (!this.IsRunning || this.Port == 0)
            {
                await Task.Delay(100);
            }
            else
            {
                isInitialized = true;
                break;
            }
        }

        return isInitialized;
    }

    private void ReadStandardError(object sender, DataReceivedEventArgs e)
    {
        Regex websocketUrlMatcher = new(@"Server listening on ws:\/\/localhost:(\d+)", RegexOptions.IgnoreCase);
        if (e.Data is not null)
        {
            Match regexMatch = websocketUrlMatcher.Match(e.Data);
            if (regexMatch.Success)
            {
                this.Port = int.Parse(regexMatch.Groups[1].Value);
            }
        }
    }

    private void ReadStandardOutput(object sender, DataReceivedEventArgs e)
    {
        Regex websocketUrlMatcher = new(@"Server listening on ws:\/\/localhost:(\d+)", RegexOptions.IgnoreCase);
        if (e.Data is not null)
        {
            Match regexMatch = websocketUrlMatcher.Match(e.Data);
            if (regexMatch.Success)
            {
                this.Port = int.Parse(regexMatch.Groups[1].Value);
            }
        }
    }

    private string GetClickerPath(LaunchOptions options)
    {
        // First, check for a path specified in code.
        if (options.ExecutablePath is not null && !string.IsNullOrEmpty(options.ExecutablePath))
        {
            return options.ExecutablePath;
        }

        // Next, check for an environment variable.
        string? envPath = Environment.GetEnvironmentVariable("CLICKER_PATH");
        if (!string.IsNullOrEmpty(envPath) && File.Exists(envPath))
        {
            return envPath;
        }

        // Finally, extract the clicker from the embedded assembly resources.
        string resourceName = $"{DefaultClickerFileName}-{this.clickerOptions.OperatingSystemName}-{this.clickerOptions.OperatingSystemArchitecture}";
        string outputFile = this.ExtractResource(resourceName);
        return outputFile;
    }

    private string ExtractResource(string resourceName)
    {
        // TODO: This is a fairly naive implementation. It is not thread-safe.
        // It extracts the clicker app from the assembly resources to the
        // Vibium cache directory if, and only if, the SHA256 hash of the
        // existing clicker app file in the cache is different from that of
        // the embedded assembly resource. Potential race conditions include
        // multiple processes accessing this executable at a time, the version
        // of the clicker app being updated while being used by another client,
        // and so on.
        string existingFileHash = string.Empty;
        string outputFile = Path.Combine(this.clickerOptions.CacheDirectory, $"{resourceName}{this.clickerOptions.FileExtension}");
        if (File.Exists(outputFile))
        {
            using FileStream existingFileStream = File.OpenRead(outputFile);
            existingFileHash = this.GetStreamHash(existingFileStream);
        }

        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        using Stream? resourceStream = executingAssembly.GetManifestResourceStream(resourceName);
        if (resourceStream is not null)
        {
            string resourceHash = this.GetStreamHash(resourceStream);
            if (existingFileHash != resourceHash)
            {
                // Only write the resource to disk if the SHA256 hash of the resource
                // differs from that of the file existing on the disk.
                resourceStream.Seek(0, SeekOrigin.Begin);
                using FileStream destinationFileStream = new(outputFile, FileMode.Create);
                resourceStream.CopyTo(destinationFileStream);
                this.SetFileExecutable(outputFile);
            }
        }

        return outputFile;
    }

    private string GetStreamHash(Stream inputStream)
    {
        using SHA256 streamHash = SHA256.Create();
        byte[] streamHashBytes = streamHash.ComputeHash(inputStream);
        return BitConverter.ToString(streamHashBytes).ToLowerInvariant().Replace("-", string.Empty);
    }

    private void SetFileExecutable(string filePath)
    {
        // TODO: Figure out a way in .NET Standard 2.0 to do this better.
        // File.SetUnixFileMode is not ported to be backward compatible with
        // .NET Standard.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            chmod(filePath, 493);
        }
    }

    private record ClickerOptions
    {
        public ClickerOptions()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                this.CacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "vibium");
                this.OperatingSystemName = "windows";
                this.FileExtension = ".exe";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                this.CacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".cache", "vibium");
                this.OperatingSystemName = "linux";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                this.CacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Library", "Caches", "vibium");
                this.OperatingSystemName = "darwin";
            }
            else
            {
                throw new VibiumException($"Unknown OS platform: {Environment.OSVersion.VersionString}");
            }

            this.OperatingSystemArchitecture = RuntimeInformation.OSArchitecture switch
            {
                Architecture.X64 => "amd64",
                Architecture.Arm64 => "arm64",
                _ => throw new VibiumException($"Unknown OS platform: {RuntimeInformation.OSArchitecture}"),
            };
        }

        public string FileExtension { get; } = string.Empty;

        public string CacheDirectory { get; }

        public string OperatingSystemName { get; }

        public string OperatingSystemArchitecture { get; }
    }
}
