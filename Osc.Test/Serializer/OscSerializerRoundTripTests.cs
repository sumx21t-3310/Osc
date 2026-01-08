using System.Text;
using Osc.Protocol;
using Osc.Serializer;
using Xunit.Abstractions;

namespace Osc.Test.Serializer;

public class OscSerializerRoundTripTests(ITestOutputHelper outputHelper)
{
    private readonly IOscSerializer _serializer = new JsonOscSerializer();

    [Fact]
    public void Serialize_then_Deserialize_roundtrip()
    {
        var payload = new OscPayload(
            "/test",
            [
                new OscArgument(OscType.Int32, 42),
                new OscArgument(OscType.Float32, 0.5f),
                new OscArgument(OscType.String, "hello")
            ]);


        var bytes = _serializer.Serialize(payload);
        var result = _serializer.Deserialize(bytes);
        outputHelper.WriteLine(Encoding.UTF8.GetString(bytes));
        Assert.Equal("/test", result.Address);
        Assert.Equal(3, result.Arguments.Length);

        Assert.Equal(42, (int)result.Arguments[0].Argument);
        Assert.Equal(0.5f, (float)result.Arguments[1].Argument, 4);
        Assert.Equal("hello", result.Arguments[2].Argument);
    }
}