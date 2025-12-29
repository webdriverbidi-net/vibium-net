// <copyright file="Element.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium;

using Vibium.Driver;
using WebDriverBiDi.Script;

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

    /// <summary>
    /// Gets the text of the element.
    /// </summary>
    /// <returns>The text of the element.</returns>
    /// <exception cref="VibiumException">Thrown when there is an error retrieving the element text.</exception>
    public async Task<string> GetTextAsync()
    {
        string functionDeclaration = """
                                     (selector) => {
                                       const el = document.querySelector(selector);
                                       return el ? (el.textContent || '').trim() : null;
                                     }
                                     """;
        Target functionTarget = new ContextTarget(this.browsingContextId);
        CallFunctionCommandParameters callFunctionParameters = new(functionDeclaration, functionTarget, false)
        {
            ResultOwnership = ResultOwnership.Root,
        };
        callFunctionParameters.Arguments.Add(LocalValue.String(this.selector));
        EvaluateResult result = await this.driver.Script.CallFunctionAsync(callFunctionParameters);
        if (result is EvaluateResultException exceptionResult)
        {
            throw new VibiumException($"Unexpected error in executing function: {exceptionResult.ExceptionDetails.Text}");
        }

        EvaluateResultSuccess successResult = (EvaluateResultSuccess)result;

        if (successResult.Result.Type == "null")
        {
            throw new VibiumException($"Element not found with selector {this.selector}");
        }

        // We know we have a non-null result, so we can use the null-forgiving operator here.
        return successResult.Result.ValueAs<string>()!;
    }

    /// <summary>
    /// Gets the value of an attribute of the element.
    /// </summary>
    /// <param name="attributeName">The name of the attribute whose value to get.</param>
    /// <returns>The value of the attribute, or <see langword="null"/> if the element or attribute does not exist.</returns>
    /// <exception cref="VibiumException">Thrown when there is an error retrieving the element text.</exception>
    public async Task<string?> GetAttributeAsync(string attributeName)
    {
        string functionDeclaration = """
                                     (selector, attrName) => {
                                       const el = document.querySelector(selector);
                                       return el ? el.getAttribute(attrName) : null;
                                     }
                                     """;
        Target functionTarget = new ContextTarget(this.browsingContextId);
        CallFunctionCommandParameters callFunctionParameters = new(functionDeclaration, functionTarget, false)
        {
            ResultOwnership = ResultOwnership.Root,
        };
        callFunctionParameters.Arguments.AddRange(LocalValue.String(this.selector), LocalValue.String(attributeName));
        EvaluateResult result = await this.driver.Script.CallFunctionAsync(callFunctionParameters);
        if (result is EvaluateResultException exceptionResult)
        {
            throw new VibiumException($"Unexpected error in executing function: {exceptionResult.ExceptionDetails.Text}");
        }

        EvaluateResultSuccess successResult = (EvaluateResultSuccess)result;

        if (successResult.Result.Type == "null")
        {
            return null;
        }

        // We know we have a non-null result, so we can use the null-forgiving operator here.
        return successResult.Result.ValueAs<string>()!;
    }
}
