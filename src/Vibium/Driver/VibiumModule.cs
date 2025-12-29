// <copyright file="VibiumModule.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vibium.Driver;

using WebDriverBiDi;

/// <summary>
/// The Vibium module contains commands and events relating to using the Vibium custom WebDriver BiDi proxy.
/// </summary>
public class VibiumModule : Module
{
    /// <summary>
    /// The name of the vibium module.
    /// </summary>
    public const string VibiumModuleName = "vibium";

    /// <summary>
    /// Initializes a new instance of the <see cref="VibiumModule"/> class.
    /// </summary>
    /// <param name="driver">The <see cref="BiDiDriver"/> used in the module commands and events.</param>
    public VibiumModule(BiDiDriver driver)
       : base(driver)
    {
    }

    /// <summary>
    /// Gets the module name.
    /// </summary>
    public override string ModuleName => VibiumModuleName;

    /// <summary>
    /// Finds an element in the browsing context being automated.
    /// </summary>
    /// <param name="commandProperties">The parameters for the command.</param>
    /// <returns>The result object describing the found element.</returns>
    public async Task<FindCommandResult> FindAsync(FindCommandParameters commandProperties)
    {
        return await this.Driver.ExecuteCommandAsync<FindCommandResult>(commandProperties).ConfigureAwait(false);
    }

    /// <summary>
    /// Clicks on an element in the browsing context being automated.
    /// </summary>
    /// <param name="commandProperties">The parameters for the command.</param>
    /// <returns>The result object describing the result of the command.</returns>
    public async Task<ClickCommandResult> ClickAsync(ClickCommandParameters commandProperties)
    {
        return await this.Driver.ExecuteCommandAsync<ClickCommandResult>(commandProperties).ConfigureAwait(false);
    }

    /// <summary>
    /// Types into an element in the browsing context being automated.
    /// </summary>
    /// <param name="commandProperties">The parameters for the command.</param>
    /// <returns>The result object describing the result of the command.</returns>
    public async Task<TypeCommandResult> TypeAsync(TypeCommandParameters commandProperties)
    {
        return await this.Driver.ExecuteCommandAsync<TypeCommandResult>(commandProperties).ConfigureAwait(false);
    }
}