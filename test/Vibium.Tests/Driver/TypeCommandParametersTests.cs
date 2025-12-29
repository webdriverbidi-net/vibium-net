namespace Vibium.Driver;

using System.Text.Json;
using Newtonsoft.Json.Linq;

[TestFixture]
public class TypeCommandParametersTests
{
    [Test]
    public void TestCommandName()
    {
        TypeCommandParameters properties = new("myContextId", "mySelector", "myText");
        Assert.That(properties.MethodName, Is.EqualTo("vibium:type"));
    }

    [Test]
    public void TestCanSerialize()
    {
        TypeCommandParameters properties = new("myContextId", "mySelector", "myText");
        string json = JsonSerializer.Serialize(properties);
        JObject serialized = JObject.Parse(json);
        Assert.That(serialized, Has.Count.EqualTo(3));
        using (Assert.EnterMultipleScope())
        {
            Assert.That(serialized, Contains.Key("context"));
            Assert.That(serialized["context"]!.Type, Is.EqualTo(JTokenType.String));
            Assert.That(serialized["context"]!.Value<string>(), Is.EqualTo("myContextId"));
            Assert.That(serialized, Contains.Key("selector"));
            Assert.That(serialized["selector"]!.Type, Is.EqualTo(JTokenType.String));
            Assert.That(serialized["selector"]!.Value<string>(), Is.EqualTo("mySelector"));
            Assert.That(serialized, Contains.Key("text"));
            Assert.That(serialized["text"]!.Type, Is.EqualTo(JTokenType.String));
            Assert.That(serialized["text"]!.Value<string>(), Is.EqualTo("myText"));
        }
    }

    [Test]
    public void TestCanSerializeWithTimeout()
    {
        TypeCommandParameters properties = new("myContextId", "mySelector", "myText")
        {
            Timeout = TimeSpan.FromMilliseconds(100),
        };
        string json = JsonSerializer.Serialize(properties);
        JObject serialized = JObject.Parse(json);
        Assert.That(serialized, Has.Count.EqualTo(4));
        using (Assert.EnterMultipleScope())
        {
            Assert.That(serialized, Contains.Key("context"));
            Assert.That(serialized["context"]!.Type, Is.EqualTo(JTokenType.String));
            Assert.That(serialized["context"]!.Value<string>(), Is.EqualTo("myContextId"));
            Assert.That(serialized, Contains.Key("selector"));
            Assert.That(serialized["selector"]!.Type, Is.EqualTo(JTokenType.String));
            Assert.That(serialized["selector"]!.Value<string>(), Is.EqualTo("mySelector"));
            Assert.That(serialized, Contains.Key("text"));
            Assert.That(serialized["text"]!.Type, Is.EqualTo(JTokenType.String));
            Assert.That(serialized["text"]!.Value<string>(), Is.EqualTo("myText"));
            Assert.That(serialized, Contains.Key("timeout"));
            Assert.That(serialized["timeout"]!.Type, Is.EqualTo(JTokenType.Integer));
            Assert.That(serialized["timeout"]!.Value<uint>(), Is.EqualTo(100));
        }
    }
}
