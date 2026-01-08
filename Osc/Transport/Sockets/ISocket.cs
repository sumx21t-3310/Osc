using Osc.Protocol;

namespace Osc.Transport.Sockets;

public interface ISocket : IDisposable
{
    Task<ReceivedPacket> ReceiveAsync(CancellationToken token);
    Task SendAsync(byte[] payload, CancellationToken token);
}