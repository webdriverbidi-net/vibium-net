namespace Vibium.Driver;

using System.Text.Json;
using WebDriverBiDi.JsonConverters;

[TestFixture]
public class ClickCommandResultTests
{
    private JsonSerializerOptions deserializationOptions = new()
    {
        TypeInfoResolver = new PrivateConstructorContractResolver(),
    };

    [Test]
    public void TestCanDeserialize()
    {
        ClickCommandResult? result = JsonSerializer.Deserialize<ClickCommandResult>("{}", deserializationOptions);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.AdditionalData, Is.Empty);
    }

    [Test]
    public void TestCopySemantics()
    {
        ClickCommandResult? result = JsonSerializer.Deserialize<ClickCommandResult>("{}", deserializationOptions);
        Assert.That(result, Is.Not.Null);
        ClickCommandResult copy = result with { };
        Assert.That(copy, Is.EqualTo(result));
    }
}
