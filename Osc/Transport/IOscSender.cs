using Osc.Protocol;

namespace Osc;

public interface IOscSender : IDisposable
{
    Task SendAsync(OscPayload oscPayload, CancellationToken cancellationToken);
}