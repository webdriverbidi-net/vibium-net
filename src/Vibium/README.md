# Vibium
A .NET client library for the Vibium project

This package contains a library that is a .NET client for the
[Vibium](https://vibium.com/) library.

**Important!** This library is experimental, and not an official part
of the Vibium project.

## Quick Start
To use the library, you can use code similar to the following:

```c-sharp
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
```
