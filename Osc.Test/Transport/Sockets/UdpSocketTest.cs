using System.Net;
using System.Net.Sockets;
using Osc.Transport.Sockets;

namespace Osc.Test.Transport.Sockets;

public class UdpSocketTests : SocketCancellationContractTests
{
    protected override ISocket CreateSocket()
    {
        var client = new UdpClient(0);
        var endPoint = new IPEndPoint(IPAddress.Loopback, 12345);
        return new UdpSocket(client, endPoint);
    }
}