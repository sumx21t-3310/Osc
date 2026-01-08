using Osc.Transport.Sockets;

namespace Osc.Test.Transport.Sockets;

public class LoopbackSocketTests : SocketContractTests
{
    protected override ISocket CreateSocket() => new LoopbackSocket();
}