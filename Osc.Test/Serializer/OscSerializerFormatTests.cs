using System.Text;
using Osc.Protocol;
using Osc.Serializer;

namespace Osc.Test.Serializer;

public class OscSerializerFormatTests
{
    private readonly IOscSerializer _serializer = new JsonOscSerializer();

    [Fact]
    public void Address_is_null_terminated_and_4byte_aligned()
    {
        var bytes = _serializer.Serialize(
            new OscPayload("/a", Array.Empty<OscArgument>()));

        Assert.Equal((byte)'/', bytes[0]);
        Assert.Equal((byte)'a', bytes[1]);
        Assert.Equal(0, bytes[2]);
        Assert.Equal(0, bytes[3]);
    }

    [Fact]
    public void Type_tag_string_is_correct()
    {
        var payload = new OscPayload(
            "/x",
            new[]
            {
                new OscArgument(OscType.Int32, 1),
                new OscArgument(OscType.Float32, 2f),
                new OscArgument(OscType.String, "s"),
            });

        var ascii = Encoding.ASCII.GetString(
            _serializer.Serialize(payload));

        Assert.Contains(",ifs", ascii);
    }

    [Fact]
    public void Int32_is_big_endian()
    {
        var payload = new OscPayload(
            "/i",
            new[] { new OscArgument(OscType.Int32, 0x01020304) });

        var bytes = _serializer.Serialize(payload);
        var index = Array.IndexOf(bytes, (byte)'i') + 4;

        Assert.Equal(0x01, bytes[index + 0]);
        Assert.Equal(0x02, bytes[index + 1]);
        Assert.Equal(0x03, bytes[index + 2]);
        Assert.Equal(0x04, bytes[index + 3]);
    }

    [Fact]
    public void Float32_is_big_endian_ieee754()
    {
        var payload = new OscPayload(
            "/f",
            new[] { new OscArgument(OscType.Float32, 1.0f) });

        var bytes = _serializer.Serialize(payload);
        var index = Array.IndexOf(bytes, (byte)'f') + 4;

        Assert.Equal(0x3F, bytes[index + 0]);
        Assert.Equal(0x80, bytes[index + 1]);
        Assert.Equal(0x00, bytes[index + 2]);
        Assert.Equal(0x00, bytes[index + 3]);
    }
}