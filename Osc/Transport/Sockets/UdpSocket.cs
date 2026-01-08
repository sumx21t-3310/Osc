using System.Net;
using System.Net.Sockets;
using Osc.Protocol;

namespace Osc.Transport.Sockets;

public class UdpSocket(UdpClient udpClient, IPEndPoint endPoint) : ISocket
{
    public async Task<ReceivedPacket> ReceiveAsync(CancellationToken token)
    {
        var result = await udpClient.ReceiveAsync(token);
        return result;
    }

    public async Task SendAsync(byte[] payload, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        await udpClient.SendAsync(payload, endPoint, token);
    }

    public void Dispose() => udpClient.Dispose();
}