namespace MastoAltText;

using IntelligenceEngine;

using ListenerEngine;

using StoreEngine;

using System.Data.SqlTypes;

public class Worker : IHostedService
{
	private readonly ILogger<Worker> logger;
	private readonly IListener listener;
	private readonly IIntelligence intelligenceEngine;
	private readonly IStore store;

	public Worker(ILogger<Worker> logger, IListener listener, IIntelligence intelligenceEngine, IStore store)
	{
		this.logger = logger;
		this.listener = listener;
		this.intelligenceEngine = intelligenceEngine;
		this.store = store;
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		logger.LogInformation("Worker running");
		listener.NewMediaToot += async (sender, e) =>
		{
			await store.SaveMediaTootAsync(e.MediaToot);
			var message = await intelligenceEngine.GetMessage(e.MediaToot.AccountId);
		};
		listener.Start();
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		listener.Dispose();
		logger.LogInformation("Worker stoped");
		return Task.CompletedTask;
	}
}
