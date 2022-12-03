namespace MastoAltText;
using ListenerEngine;

public class Worker : IHostedService
{
    private readonly ILogger<Worker> Logger;
    private readonly IListener Listener;

    public Worker(ILogger<Worker> logger, IListener listener)
    {
        Logger = logger;
        Listener = listener;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("Worker running");        
        Listener.Start();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Listener.Stop();
        Logger.LogInformation("Worker stoped");
        return Task.CompletedTask;
    }
}
