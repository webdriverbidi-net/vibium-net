// <copyright file="Element.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium;

using Vibium.Driver;

/// <summary>
/// Contains methods for interacting with an element.
/// </summary>
public class Element
{
    private VibiumDriver driver;
    private string browsingContextId;
    private string selector;
    private ElementInfo info;

    /// <summary>
    /// Initializes a new instance of the <see cref="Element"/> class.
    /// </summary>
    /// <param name="driver">The <see cref="VibiumDriver"/> instance driving the browser.</param>
    /// <param name="browsingContextId">The ID of the browsing context containing the element.</param>
    /// <param name="selector">The CSS selector used to locate the element.</param>
    /// <param name="info">The <see cref="ElementInfo"/> object containing info about the element.</param>
    public Element(VibiumDriver driver, string browsingContextId, string selector, ElementInfo info)
    {
        this.driver = driver;
        this.browsingContextId = browsingContextId;
        this.selector = selector;
        this.info = info;
    }

    /// <summary>
    /// Clicks on the element.
    /// </summary>
    /// <param name="options">The <see cref="ActionOptions"/> object containing options for clicking.</param>
    /// <returns>An empty result.</returns>
    public async Task ClickAsync(ActionOptions? options = null)
    {
        ClickCommandParameters clickParameters = new(this.browsingContextId, this.selector);
        if (options is not null)
        {
            clickParameters.Timeout = options.Timeout;
        }

        await this.driver.Vibium.ClickAsync(clickParameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Types into the element.
    /// </summary>
    /// <param name="text">The text to type into the element.</param>
    /// <param name="options">The <see cref="ActionOptions"/> object containing options for typing.</param>
    /// <returns>An empty result.</returns>
    public async Task TypeAsync(string text, ActionOptions? options = null)
    {
        TypeCommandParameters typeParameters = new(this.browsingContextId, this.selector, text);
        if (options is not null)
        {
            typeParameters.Timeout = options.Timeout;
        }

        await this.driver.Vibium.TypeAsync(typeParameters).ConfigureAwait(false);
    }
}
