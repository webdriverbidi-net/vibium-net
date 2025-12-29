// <copyright file="FindCommandResult.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium.Driver;

using System.Text.Json.Serialization;
using WebDriverBiDi;

/// <summary>
/// Result for finding an element using the vibium:find command.
/// </summary>
public record FindCommandResult : CommandResult
{
    [JsonConstructor]
    private FindCommandResult()
    {
    }

    /// <summary>
    /// Gets the tag of the found element.
    /// </summary>
    [JsonPropertyName("tag")]
    [JsonRequired]
    [JsonInclude]
    public string Tag { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the text of the found element.
    /// </summary>
    [JsonPropertyName("text")]
    [JsonRequired]
    [JsonInclude]
    public string Text { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the bounding client rect of the found element.
    /// </summary>
    [JsonPropertyName("box")]
    [JsonRequired]
    [JsonInclude]
    public Box Box { get; private set; } = Box.Default;
}