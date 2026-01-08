using System.Net;
using System.Threading.Channels;
using Osc.Protocol;

namespace Osc.Transport.Sockets;

public class LoopbackSocket : ISocket
{
    private readonly Channel<byte[]> _channel = Channel.CreateUnbounded<byte[]>();


    public async Task<ReceivedPacket> ReceiveAsync(CancellationToken token)
    {
        var buffer = await _channel.Reader.ReadAsync(token);

        return new ReceivedPacket(buffer, new IPEndPoint(IPAddress.Loopback, 9999));
    }

    public async Task SendAsync(byte[] payload, CancellationToken token) =>
        await _channel.Writer.WriteAsync(payload, token);

    public void Dispose() => _channel.Writer.TryComplete();
}