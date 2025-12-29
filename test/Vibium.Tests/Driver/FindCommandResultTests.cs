namespace Vibium.Driver;

using System.Text.Json;
using WebDriverBiDi.JsonConverters;

[TestFixture]
public class FindCommandResultTests
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
                        "tag": "myTag",
                        "text": "myText",
                        "box": {
                          "x": 100,
                          "y": 200,
                          "width": 300,
                          "height": 400
                        }
                      }
                      """;
        FindCommandResult? result = JsonSerializer.Deserialize<FindCommandResult>(json, deserializationOptions);
        Assert.That(result, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.Tag, Is.EqualTo("myTag"));
            Assert.That(result.Text, Is.EqualTo("myText"));
            Assert.That(result.Box.X, Is.EqualTo(100));
            Assert.That(result.Box.Y, Is.EqualTo(200));
            Assert.That(result.Box.Width, Is.EqualTo(300));
            Assert.That(result.Box.Height, Is.EqualTo(400));
        }
        Assert.That(result.AdditionalData, Is.Empty);
    }

    [Test]
    public void TestDeserializingWithMissingTagThrows()
    {
        string json = """
                      {
                        "text": "myText",
                        "box": {
                          "x": 100,
                          "y": 200,
                          "width": 300,
                          "height": 400
                        }
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<FindCommandResult>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
    }

    [Test]
    public void TestDeserializingWithInvalidTagTypeThrows()
    {
        string json = """
                      {
                        "tag": {},
                        "text": "myText",
                        "box": {
                          "x": 100,
                          "y": 200,
                          "width": 300,
                          "height": 400
                        }
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<FindCommandResult>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
    }

    [Test]
    public void TestDeserializingWithMissingTextThrows()
    {
        string json = """
                      {
                        "tag": "myTag",
                        "box": {
                          "x": 100,
                          "y": 200,
                          "width": 300,
                          "height": 400
                        }
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<FindCommandResult>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
    }

    [Test]
    public void TestDeserializingWithInvalidTextTypeThrows()
    {
        string json = """
                      {
                        "tag": "myTag",
                        "text": {},
                        "box": {
                          "x": 100,
                          "y": 200,
                          "width": 300,
                          "height": 400
                        }
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<FindCommandResult>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
    }

    [Test]
    public void TestDeserializingWithMissingBoxThrows()
    {
        string json = """
                      {
                        "tag": "myTag",
                        "text": "myText"
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<FindCommandResult>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
    }

    [Test]
    public void TestDeserializingWithInvalidBoxTypeThrows()
    {
        string json = """
                      {
                        "tag": "myTag",
                        "text": "myText",
                        "box": ""
                      }
                      """;
        Assert.That(() => JsonSerializer.Deserialize<FindCommandResult>(json, deserializationOptions), Throws.InstanceOf<JsonException>());
    }

    [Test]
    public void TestCopySemantics()
    {
        string json = """
                      {
                        "tag": "myTag",
                        "text": "myText",
                        "box": {
                          "x": 100,
                          "y": 200,
                          "width": 300,
                          "height": 400
                        }
                      }
                      """;
        FindCommandResult? result = JsonSerializer.Deserialize<FindCommandResult>(json, deserializationOptions);
        Assert.That(result, Is.Not.Null);
        FindCommandResult copy = result with { };
        Assert.That(copy, Is.EqualTo(result));
    }
}
