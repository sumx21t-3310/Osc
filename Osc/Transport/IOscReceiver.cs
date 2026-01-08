using Osc.Protocol;

namespace Osc;

public interface IOscReceiver
{
    Task<OscPayload> ReceiveAsync(CancellationToken cancellationToken);
}