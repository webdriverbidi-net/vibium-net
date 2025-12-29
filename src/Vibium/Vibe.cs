// <copyright file="Vibe.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium;

using Vibium.Driver;
using WebDriverBiDi.BrowsingContext;

/// <summary>
/// Contains methods for interacting with a browsing context.
/// </summary>
public class Vibe
{
    private readonly VibiumDriver driver;
    private readonly Browser launcher;
    private string context = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="Vibe"/> class.
    /// </summary>
    /// <param name="driver">The <see cref="VibiumDriver"/> used to drive the browser.</param>
    /// <param name="launcher">The <see cref="Browser"/> launcher used to launch the browser.</param>
    public Vibe(VibiumDriver driver, Browser launcher)
    {
        this.driver = driver;
        this.launcher = launcher;
    }

    /// <summary>
    /// Navigates to a URL.
    /// </summary>
    /// <param name="url">The URL to which to navigate.</param>
    /// <returns>An empty result.</returns>
    public async Task GoAsync(string url)
    {
        string context = await this.GetContextAsync().ConfigureAwait(false);
        NavigateCommandParameters navigateParameters = new(context, url)
        {
            Wait = ReadinessState.Complete,
        };
        await this.driver.BrowsingContext.NavigateAsync(navigateParameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a screenshot of the current browsing context.
    /// </summary>
    /// <returns>A byte array representing a PNG image containing a screenshot of the current page.</returns>
    public async Task<byte[]> ScreenshotAsync()
    {
        string context = await this.GetContextAsync().ConfigureAwait(false);
        CaptureScreenshotCommandResult screenshotResult = await this.driver.BrowsingContext.CaptureScreenshotAsync(new CaptureScreenshotCommandParameters(context)).ConfigureAwait(false);
        return Convert.FromBase64String(screenshotResult.Data);
    }

    /// <summary>
    /// Finds an element.
    /// </summary>
    /// <param name="selector">The selector to use to find the element.</param>
    /// <param name="options">The options used in finding the element.</param>
    /// <returns>The <see cref="Element"/> object that represents the element on the page.</returns>
    public async Task<Element> FindAsync(string selector, FindOptions? options = null)
    {
        string context = await this.GetContextAsync().ConfigureAwait(false);
        FindCommandParameters findParameters = new(context, selector);
        if (options is not null)
        {
            findParameters.Timeout = options.Timeout;
        }

        FindCommandResult findResult = await this.driver.Vibium.FindAsync(findParameters);
        return new Element(this.driver, context, selector, new ElementInfo(findResult));
    }

    /// <summary>
    /// Quits the browser and launcher.
    /// </summary>
    /// <returns>An empty result.</returns>
    public async Task QuitAsync()
    {
        await this.driver.StopAsync();
        await this.launcher.QuitAsync();
    }

    private async Task<string> GetContextAsync()
    {
        if (string.IsNullOrEmpty(this.context))
        {
            GetTreeCommandResult getTreeResult = await this.driver.BrowsingContext.GetTreeAsync(new GetTreeCommandParameters());
            IList<BrowsingContextInfo> tree = getTreeResult.ContextTree;
            if (tree.Count == 0)
            {
                throw new VibiumException("No browsing context available");
            }

            this.context = tree[0].BrowsingContextId;
        }

        return this.context;
    }
}
