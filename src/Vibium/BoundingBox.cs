// <copyright file="BoundingBox.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium;

using Vibium.Driver;

/// <summary>
/// Contains the coordinates of the bounding box of an element.
/// </summary>
public record BoundingBox
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BoundingBox"/> class.
    /// </summary>
    /// <param name="input">The <see cref="Box"/> object containing coordinates of the box.</param>
    public BoundingBox(Box input)
    {
        this.X = input.X;
        this.Y = input.Y;
        this.Height = input.Height;
        this.Width = input.Width;
    }

    /// <summary>
    /// Gets the X coordinate of the bounding box.
    /// </summary>
    public double X { get; }

    /// <summary>
    /// Gets the Y coordinate of the bounding box.
    /// </summary>
    public double Y { get; }

    /// <summary>
    /// Gets the width of the bounding box.
    /// </summary>
    public double Width { get; }

    /// <summary>
    /// Gets the height of the bounding box.
    /// </summary>
    public double Height { get; }
}