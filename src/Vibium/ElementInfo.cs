// <copyright file="ElementInfo.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium;

using Vibium.Driver;

/// <summary>
/// A data structure containing information about an element.
/// </summary>
public record ElementInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ElementInfo"/> class.
    /// </summary>
    /// <param name="result">The result of the find command containing the element info.</param>
    public ElementInfo(FindCommandResult result)
    {
        this.Tag = result.Tag;
        this.Text = result.Text;
        this.Box = new BoundingBox(result.Box);
    }

    /// <summary>
    /// Gets the tag name of the element.
    /// </summary>
    public string Tag { get; }

    /// <summary>
    /// Gets the text of the element.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Gets the rect representing the size and position of the element.
    /// </summary>
    public BoundingBox Box { get; }
}