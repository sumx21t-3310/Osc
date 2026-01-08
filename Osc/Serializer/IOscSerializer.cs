using Osc.Protocol;

namespace Osc.Serializer;

public interface IOscSerializer
{
    byte[] Serialize(OscPayload payload);
    OscPayload Deserialize(byte[] data);
}