// <copyright file="TypeCommandParameters.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium.Driver;

using System.Text.Json.Serialization;
using WebDriverBiDi;

/// <summary>
/// Provides parameters for the vibium:click command.
/// </summary>
public class TypeCommandParameters : CommandParameters<TypeCommandResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TypeCommandParameters"/> class.
    /// </summary>
    /// <param name="browsingContextId">The ID of the browsing context to search for the element to type into.</param>
    /// <param name="selector">The CSS selector used to search for the element to type into.</param>
    /// <param name="text">The text to type into the element.</param>
    public TypeCommandParameters(string browsingContextId, string selector, string text)
    {
        this.BrowsingContextId = browsingContextId;
        this.Selector = selector;
        this.Text = text;
    }

    /// <summary>
    /// Gets the method name of the command.
    /// </summary>
    [JsonIgnore]
    public override string MethodName => "vibium:type";

    /// <summary>
    /// Gets or sets the ID of the browsing context in which to type in an element.
    /// </summary>
    [JsonPropertyName("context")]
    [JsonInclude]
    public string BrowsingContextId { get; set; }

    /// <summary>
    /// Gets or sets the selector to be used when typing in an element.
    /// </summary>
    [JsonPropertyName("selector")]
    [JsonInclude]
    public string Selector { get; set; }

    /// <summary>
    /// Gets or sets the text to be typed into an element.
    /// </summary>
    [JsonPropertyName("text")]
    [JsonInclude]
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets the time span to wait for the typing to be executed.
    /// </summary>
    [JsonIgnore]
    public TimeSpan? Timeout { get; set; }

    /// <summary>
    /// Gets the time span to wait for the typing to be executed in milliseconds for serialization purposes.
    /// </summary>
    [JsonPropertyName("timeout")]
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    internal uint? SerializableTimeout
    {
        get
        {
            if (this.Timeout is null)
            {
                return null;
            }

            return Convert.ToUInt32(this.Timeout.Value.TotalMilliseconds);
        }
    }
}