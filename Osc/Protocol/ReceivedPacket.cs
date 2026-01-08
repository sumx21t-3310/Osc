using System.Net;
using System.Net.Sockets;

namespace Osc.Protocol;

public record struct ReceivedPacket(byte[] Buffer, IPEndPoint RemoteEndPoint)
{
    public static implicit operator byte[](ReceivedPacket packet) => packet.Buffer;

    public static implicit operator ReceivedPacket(UdpReceiveResult receiveResult) =>
        new(receiveResult.Buffer, receiveResult.RemoteEndPoint);
}