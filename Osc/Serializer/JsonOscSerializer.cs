using System.Text;
using System.Text.Json;
using Osc.Protocol;

namespace Osc.Serializer;

public class JsonOscSerializer : IOscSerializer
{
    public byte[] Serialize(OscPayload payload)
    {
        var json = JsonSerializer.Serialize(payload);
        return Encoding.UTF8.GetBytes(json);
    }

    public OscPayload Deserialize(byte[] data)
    {
        var json = Encoding.UTF8.GetString(data);
        var payload = JsonSerializer.Deserialize<OscPayload>(json);

        if (payload == null) return new OscPayload("/", []);

        var mappedArguments = payload.Arguments.Select(arg =>
        {
            if (arg.Argument is JsonElement element)
            {
                object value = arg.Tag switch
                {
                    OscType.Int32 => element.GetInt32(),
                    OscType.Float32 => element.GetSingle(),
                    OscType.String => element.GetString() ?? "",
                    _ => element.GetRawText()
                };
                return arg with { Argument = value };
            }

            return arg;
        }).ToArray();

        return payload with { Arguments = mappedArguments };
    }
}