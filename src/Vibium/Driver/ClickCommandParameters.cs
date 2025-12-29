// <copyright file="ClickCommandParameters.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium.Driver;

using System.Text.Json.Serialization;
using WebDriverBiDi;

/// <summary>
/// Provides parameters for the vibium:click command.
/// </summary>
public class ClickCommandParameters : CommandParameters<ClickCommandResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClickCommandParameters"/> class.
    /// </summary>
    /// <param name="browsingContextId">The ID of the browsing context to search.</param>
    /// <param name="selector">The CSS selector used to search.</param>
    public ClickCommandParameters(string browsingContextId, string selector)
    {
        this.BrowsingContextId = browsingContextId;
        this.Selector = selector;
    }

    /// <summary>
    /// Gets the method name of the command.
    /// </summary>
    [JsonIgnore]
    public override string MethodName => "vibium:click";

    /// <summary>
    /// Gets or sets the ID of the browsing context in which to click on an element.
    /// </summary>
    [JsonPropertyName("context")]
    [JsonInclude]
    public string BrowsingContextId { get; set; }

    /// <summary>
    /// Gets or sets the selector to be used when clicking on an element.
    /// </summary>
    [JsonPropertyName("selector")]
    [JsonInclude]
    public string Selector { get; set; }

    /// <summary>
    /// Gets or sets the time span to wait for the click to be executed.
    /// </summary>
    [JsonIgnore]
    public TimeSpan? Timeout { get; set; }

    /// <summary>
    /// Gets the time span to wait for the click to be executed in milliseconds for serialization purposes.
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