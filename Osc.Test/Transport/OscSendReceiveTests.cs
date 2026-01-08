using Osc.Protocol;
using Osc.Serializer;
using Osc.Transport.Sockets;

namespace Osc.Test.Transport;

public class OscSendReceiveTests
{
    [Fact]
    public async Task SendAsync_then_ReceiveAsync_returns_same_payload()
    {
        var socket = new LoopbackSocket();
        var serializer = new JsonOscSerializer();

        using var sender = new OscSender(socket, serializer);
        var receiver = new OscReceiver(socket, serializer);

        var payload = new OscPayload("/test", Array.Empty<OscArgument>());

        await sender.SendAsync(payload, CancellationToken.None);
        var received = await receiver.ReceiveAsync(CancellationToken.None);

        Assert.Equal("/test", received.Address);
    }
}