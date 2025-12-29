// <copyright file="FindOptions.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium.Driver;

/// <summary>
/// Class providing options for finding elements.
/// </summary>
public class FindOptions
{
    /// <summary>
    /// Gets or sets the timeout for finding an element.
    /// </summary>
    public TimeSpan? Timeout { get; set; }
}