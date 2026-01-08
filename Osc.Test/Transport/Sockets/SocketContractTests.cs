using Osc.Transport.Sockets;

namespace Osc.Test.Transport.Sockets;

public abstract class SocketContractTests
{
    protected abstract ISocket CreateSocket();

    [Fact]
    public async Task Send_then_Receive_returns_same_payload()
    {
        using var socket = CreateSocket();
        var payload = new byte[] { 1, 2, 3 };

        await socket.SendAsync(payload, CancellationToken.None);
        var received = await socket.ReceiveAsync(CancellationToken.None);

        Assert.Equal(payload, received.Buffer);
    }

    [Fact]
    public async Task ReceiveAsync_is_cancelable()
    {
        using var socket = CreateSocket();
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => socket.ReceiveAsync(cts.Token));
    }
}