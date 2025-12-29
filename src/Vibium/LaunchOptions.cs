// <copyright file="LaunchOptions.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium;

/// <summary>
/// An object containing the options used to launch the Vibium driver.
/// </summary>
public class LaunchOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether to run the browser in a "headless" or windowless mode.
    /// </summary>
    public bool Headless { get; set; } = false;

    /// <summary>
    /// Gets or sets the port used to communicate with the Vibium proxy server.
    /// </summary>
    public uint Port { get; set; } = 0;

    /// <summary>
    /// Gets or sets the path the the Vibium proxy executable.
    /// </summary>
    public string? ExecutablePath { get; set; }
}