namespace Vibium.Driver;

using System.Text.Json;
using Newtonsoft.Json.Linq;

[TestFixture]
public class FindCommandParametersTests
{
    [Test]
    public void TestCommandName()
    {
        FindCommandParameters properties = new("myContextId", "mySelector");
        Assert.That(properties.MethodName, Is.EqualTo("vibium:find"));
    }

    [Test]
    public void TestCanSerialize()
    {
        FindCommandParameters properties = new("myContextId", "mySelector");
        string json = JsonSerializer.Serialize(properties);
        JObject serialized = JObject.Parse(json);
        Assert.That(serialized, Has.Count.EqualTo(2));
        using (Assert.EnterMultipleScope())
        {
            Assert.That(serialized, Contains.Key("context"));
            Assert.That(serialized["context"]!.Type, Is.EqualTo(JTokenType.String));
            Assert.That(serialized["context"]!.Value<string>(), Is.EqualTo("myContextId"));
            Assert.That(serialized, Contains.Key("selector"));
            Assert.That(serialized["selector"]!.Type, Is.EqualTo(JTokenType.String));
            Assert.That(serialized["selector"]!.Value<string>(), Is.EqualTo("mySelector"));
        }
    }

    [Test]
    public void TestCanSerializeWithTimeout()
    {
        FindCommandParameters properties = new("myContextId", "mySelector")
        {
            Timeout = TimeSpan.FromMilliseconds(100),
        };
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
            Assert.That(serialized, Contains.Key("timeout"));
            Assert.That(serialized["timeout"]!.Type, Is.EqualTo(JTokenType.Integer));
            Assert.That(serialized["timeout"]!.Value<uint>(), Is.EqualTo(100));
        }
    }
}
