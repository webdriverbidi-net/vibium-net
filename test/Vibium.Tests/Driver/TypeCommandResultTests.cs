namespace Vibium.Driver;

using System.Text.Json;
using WebDriverBiDi.JsonConverters;

[TestFixture]
public class TypeCommandResultTests
{
    private JsonSerializerOptions deserializationOptions = new()
    {
        TypeInfoResolver = new PrivateConstructorContractResolver(),
    };

    [Test]
    public void TestCanDeserialize()
    {
        TypeCommandResult? result = JsonSerializer.Deserialize<TypeCommandResult>("{}", deserializationOptions);
        Assert.That(result, Is.Not.Null);
        Assert.That(result.AdditionalData, Is.Empty);
    }

    [Test]
    public void TestCopySemantics()
    {
        TypeCommandResult? result = JsonSerializer.Deserialize<TypeCommandResult>("{}", deserializationOptions);
        Assert.That(result, Is.Not.Null);
        TypeCommandResult copy = result with { };
        Assert.That(copy, Is.EqualTo(result));
    }
}
