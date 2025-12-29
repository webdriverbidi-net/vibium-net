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
    Console.WriteLine("Loaded example.com");

    byte[] screenshot = await vibe.ScreenshotAsync();
    File.WriteAllBytes("example.png", screenshot);
    Console.WriteLine("Saved screenshot");

    Element link = await vibe.FindAsync("a");
    Console.WriteLine($"Found link with text {await link.GetTextAsync()}");
    await link.ClickAsync();
    Console.WriteLine("Link clicked");

    Element destinationLink = await vibe.FindAsync("h1");
    Console.WriteLine($"Found text on destination page: {await destinationLink.GetTextAsync()}");
}
finally
{
    await vibe.QuitAsync();
}
