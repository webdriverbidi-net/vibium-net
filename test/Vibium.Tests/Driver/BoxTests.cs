namespace Vibium.Driver;

using System.Text.Json;
using WebDriverBiDi.JsonConverters;

[TestFixture]
public class BoxTests
{
    private JsonSerializerOptions deserializationOptions = new()
    {
        TypeInfoResolver = new PrivateConstructorContractResolver(),
    };

    [Test]
    public void TestCanDeserialize()
    {
        string json = """
                      {
                        "x": 100,
                        "y": 200,
                        "width": 300,
                        "height": 400
                      }
                      """;
        Box? result = JsonSerializer.Deserialize<Box>(json, deserializationOptions);
        Assert.That(result, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.X, Is.EqualTo(100));
            Assert.That(result.Y, Is.EqualTo(200));
            Assert.That(result.Width, Is.EqualTo(300));
            Assert.That(result.Height, Is.EqualTo(400));
        }
    }

    [Test]
    public void TestDeserializingWithMissingXThrows()
    {
        string json = """
                      {
                        "y": 200,
                        "width": 300,
                        "height": 400
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<Box>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
    }

    [Test]
    public void TestDeserializingWithInvalidXTypeThrows()
    {
        string json = """
                      {
                        "x": {},
                        "y": 200,
                        "width": 300,
                        "height": 400
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<Box>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
    }

    [Test]
    public void TestDeserializingWithMissingYThrows()
    {
        string json = """
                      {
                        "x": 100,
                        "width": 300,
                        "height": 400
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<Box>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
    }

    [Test]
    public void TestDeserializingWithInvalidYTypeThrows()
    {
        string json = """
                      {
                        "x": 100,
                        "y": {},
                        "width": 300,
                        "height": 400
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<Box>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
    }

    [Test]
    public void TestDeserializingWithMissingWidthThrows()
    {
        string json = """
                      {
                        "x": 100,
                        "y": 200,
                        "height": 400
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<Box>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
    }

    [Test]
    public void TestDeserializingWithInvalidWidthTypeThrows()
    {
        string json = """
                      {
                        "x": 100,
                        "y": 200,
                        "width": {},
                        "height": 400
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<Box>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
    }

    [Test]
    public void TestDeserializingWithMissingHeightThrows()
    {
        string json = """
                      {
                        "x": 100,
                        "y": 200,
                        "width": 300
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<Box>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
    }

    [Test]
    public void TestDeserializingWithInvalidHeightTypeThrows()
    {
        string json = """
                      {
                        "x": 100,
                        "y": 200,
                        "width": 300,
                        "height": {}
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<Box>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
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
