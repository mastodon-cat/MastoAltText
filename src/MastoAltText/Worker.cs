namespace MastoAltText;

using IntelligenceEngine;

using ListenerEngine;

using SenderEngine;

using StoreEngine;

using System.Data.SqlTypes;

public class Worker : IHostedService
{
    private readonly ILogger<Worker> logger;
    private readonly IListener listener;
    private readonly IIntelligence intelligenceEngine;
    private readonly IStore store;
    private readonly ISender sender;

    public Worker(ILogger<Worker> logger, IListener listener, IIntelligence intelligenceEngine, IStore store, ISender sender)
    {
        this.logger = logger;
        this.listener = listener;
        this.intelligenceEngine = intelligenceEngine;
        this.store = store;
        this.sender = sender;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Worker running");
        listener.NewMediaToot += async (s, e) =>
        {
            await store.SaveMediaTootAsync(e.MediaToot);
            var message = await intelligenceEngine.GetMessage(e.MediaToot.AccountId);
            if (message == null)
            {
                return;
            }

            await sender.SendToot($"@{e.MediaToot.AccountName} {message.Message}", false, "cat");
            if (message.MessageType == MessageType.Reward && !string.IsNullOrWhiteSpace(message.PublicMessage))
            {
                await sender.SendToot(message.PublicMessage.Replace("{name}", $"@{e.MediaToot.AccountName}"), true, "cat");
            }
        };

        await listener.Start();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        listener.Dispose();
        logger.LogInformation("Worker stoped");
        return Task.CompletedTask;
    }
}
