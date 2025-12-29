// <copyright file="ActionOptions.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium.Driver;

/// <summary>
/// Class providing options for executing actions.
/// </summary>
public class ActionOptions
{
    /// <summary>
    /// Gets or sets the timeout for executing an action on an element.
    /// </summary>
    public TimeSpan? Timeout { get; set; }
}