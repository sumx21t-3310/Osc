using Osc.Protocol;
using Osc.Serializer;
using Osc.Transport;
using Osc.Transport.Sockets;

namespace Osc;

public class OscReceiver(ISocket socket, IOscSerializer serializer) : IOscReceiver
{
    public async Task<OscPayload> ReceiveAsync(CancellationToken cancellationToken)
    {
        try
        {
            var result = await socket.ReceiveAsync(cancellationToken);
            return serializer.Deserialize(result);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (ObjectDisposedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("osc receive failed", ex);
        }
    }
}