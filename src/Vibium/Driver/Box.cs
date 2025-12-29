// <copyright file="Box.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium.Driver;

using System.Text.Json.Serialization;

/// <summary>
/// Describes the bounding client rect of a found element.
/// </summary>
public record Box
{
    [JsonConstructor]
    private Box()
    {
    }

    /// <summary>
    /// Gets the default zero-sized Box.
    /// </summary>
    [JsonIgnore]
    public static Box Default => new();

    /// <summary>
    /// Gets the X coordinate of the rect.
    /// </summary>
    [JsonPropertyName("x")]
    [JsonRequired]
    [JsonInclude]
    public double X { get; private set; } = 0;

    /// <summary>
    /// Gets the Y coordinate of the rect.
    /// </summary>
    [JsonPropertyName("y")]
    [JsonRequired]
    [JsonInclude]
    public double Y { get; private set; } = 0;

    /// <summary>
    /// Gets the width of the rect.
    /// </summary>
    [JsonPropertyName("width")]
    [JsonRequired]
    [JsonInclude]
    public double Width { get; private set; } = 0;

    /// <summary>
    /// Gets the height of the rect.
    /// </summary>
    [JsonPropertyName("height")]
    [JsonRequired]
    [JsonInclude]
    public double Height { get; private set; } = 0;
}