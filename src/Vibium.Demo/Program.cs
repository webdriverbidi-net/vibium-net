// <copyright file="Program.cs" company="WebDriverBiDi.NET Committers">
// Copyright (c) WebDriverBiDi.NET Committers. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using Vibium;

Browser browser = new();
Vibe vibe = await browser.LaunchAsync(new LaunchOptions());
try
{
    await vibe.GoAsync("http://example.com");
    Element link = await vibe.FindAsync("a");
    await link.ClickAsync();
    Element destinationLink = await vibe.FindAsync("h2");
    byte[] screenshot = await vibe.ScreenshotAsync();
    File.WriteAllBytes("/Users/james.evans/Desktop/example.png", screenshot);
}
finally
{
    await vibe.QuitAsync();
}
