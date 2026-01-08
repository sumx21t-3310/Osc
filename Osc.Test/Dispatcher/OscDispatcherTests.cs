using Osc.Dispatcher;
using Osc.Protocol;
using Osc.Serializer;
using Osc.Transport;
using Osc.Transport.Sockets;

namespace Osc.Test.Dispatcher;

public class OscDispatcherTests
{
    [Fact]
    public async Task Dispatcher_calls_registered_handler()
    {
        var socket = new LoopbackSocket();
        var serializer = new JsonOscSerializer();

        var sender = new OscSender(socket, serializer);
        var receiver = new OscReceiver(socket, serializer);
        var dispatcher = new OscDispatcher(receiver);

        var called = false;

        dispatcher.RegisterHandler("/ping", _ =>
        {
            called = true;
            return Task.CompletedTask;
        });

        using var cts = new CancellationTokenSource();

        var dispatchTask = dispatcher.StartAsync(cts.Token);

        await sender.SendAsync(
            new OscPayload("/ping", Array.Empty<OscArgument>()),
            CancellationToken.None);

        await Task.Delay(50, cts.Token);
        await cts.CancelAsync();

        Assert.True(called);
        await dispatchTask.ContinueWith(_ => { }, cts.Token);
    }
}

