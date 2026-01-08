using Osc.Protocol;
using Osc.Serializer;
using Osc.Transport;
using Osc.Transport.Sockets;

namespace Osc;

public class OscSender(ISocket socket, IOscSerializer serializer) : IOscSender
{
    public async Task SendAsync(OscPayload oscPayload, CancellationToken cancellationToken)
    {
        var data = serializer.Serialize(oscPayload);

        await socket.SendAsync(data, cancellationToken);
    }

    public void Dispose() => socket.Dispose();
}