using Osc.Protocol;

namespace Osc.Dispatcher;

public class OscDispatcher : IOscDispatcher
{
    public OscDispatcher(OscReceiver receiver)
    {
        throw new NotImplementedException();
    }

    public void RegisterHandler(string address, Func<OscArgument[], Task> handler)
    {
        throw new NotImplementedException();
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}