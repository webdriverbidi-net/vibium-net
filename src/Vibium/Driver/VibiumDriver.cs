// <copyright file="VibiumDriver.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium.Driver;

using WebDriverBiDi;
using WebDriverBiDi.Protocol;

/// <summary>
/// Expanded WebDriver BiDi driver that can use Vibium commands.
/// </summary>
public class VibiumDriver : BiDiDriver
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VibiumDriver" /> class.
    /// </summary>
    public VibiumDriver()
        : this(Timeout.InfiniteTimeSpan, new Transport())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VibiumDriver" /> class with the specified
    /// default command wait timeout.
    /// </summary>
    /// <param name="defaultCommandWaitTimeout">The default timeout to wait for a command to complete.</param>
    public VibiumDriver(TimeSpan defaultCommandWaitTimeout)
        : this(defaultCommandWaitTimeout, new Transport())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VibiumDriver" /> class with the specified
    /// default command wait timeout and <see cref="Transport" />.
    /// </summary>
    /// <param name="defaultCommandWaitTimeout">The default timeout to wait for a command to complete.</param>
    /// <param name="transport">The protocol transport object used to communicate with the browser.</param>
    public VibiumDriver(TimeSpan defaultCommandWaitTimeout, Transport transport)
        : base(defaultCommandWaitTimeout, transport)
    {
        this.RegisterModule(new VibiumModule(this));
    }

    /// <summary>
    /// Gets the vibium module as described in the Vibium project.
    /// </summary>
    public VibiumModule Vibium => this.GetModule<VibiumModule>(VibiumModule.VibiumModuleName);
}