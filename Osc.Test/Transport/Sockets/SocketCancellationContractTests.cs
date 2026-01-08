using Osc.Transport.Sockets;

namespace Osc.Test.Transport.Sockets;

public abstract class SocketCancellationContractTests
{
    protected abstract ISocket CreateSocket();

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