using Osc.Protocol;

namespace Osc.Dispatcher;

public interface IOscDispatcher
{
    void RegisterHandler(string address, Func<OscArgument[], Task> handler);
    Task StartAsync(CancellationToken cancellationToken = default);
}