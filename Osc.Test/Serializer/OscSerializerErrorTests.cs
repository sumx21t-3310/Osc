using Osc.Protocol;
using Osc.Serializer;

namespace Osc.Test.Serializer;

public class OscSerializerErrorTests
{
    private readonly IOscSerializer _serializer = new JsonOscSerializer();

    [Fact]
    public void Deserialize_invalid_data_throws()
    {
        var invalid = new byte[] { 0x01, 0x02, 0x03 };

        Assert.ThrowsAny<Exception>(() =>
            _serializer.Deserialize(invalid));
    }

    [Fact]
    public void Message_with_no_arguments_is_valid()
    {
        var payload = new OscPayload("/empty", Array.Empty<OscArgument>());

        var bytes = _serializer.Serialize(payload);
        var result = _serializer.Deserialize(bytes);

        Assert.Equal("/empty", result.Address);
        Assert.Empty(result.Arguments);
    }
}