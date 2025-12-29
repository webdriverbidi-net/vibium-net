// <copyright file="VibiumException.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium;

using WebDriverBiDi;

/// <summary>
/// The exception thrown when a Vibium operation fails.
/// </summary>
public class VibiumException : WebDriverBiDiException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VibiumException" /> class with a given message.
    /// </summary>
    /// <param name="message">The message of the exception.</param>
    public VibiumException(string message)
        : base(message)
    {
    }
}
