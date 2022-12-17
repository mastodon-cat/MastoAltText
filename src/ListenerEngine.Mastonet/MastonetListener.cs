using Mastonet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DataClasses;
using Microsoft.Extensions.Options;
using ListenerEngine.Mastonet.Config;

namespace ListenerEngine.Mastonet;
public class MastonetListener : ListenerEngine.IListener
{
	private readonly MastonetConfig config;
	private readonly MastodonClient client;
	private readonly ILogger<MastonetListener> logger;
	private TimelineStreaming? streaming = null;
	
	public MastonetListener(IOptions<MastonetConfig> config, ILogger<MastonetListener> logger)
	{
		this.config = config.Value;
		this.logger = logger;
		client = new MastodonClient(
			this.config.Instance ?? throw new ArgumentNullException(nameof(this.config.Instance)),
			this.config.AccessToken ?? throw new ArgumentNullException(nameof(this.config.AccessToken)));
	}

	public event EventHandler<ListenerEventArgs>? NewMediaToot;

	public void Dispose()
	{
		if (this.streaming != null)
		{
			streaming.OnUpdate -= OnUpdate;
			streaming.Stop();
		}
	}

	public void Start()
	{
		streaming = client.GetPublicLocalStreaming();
		streaming.OnUpdate += OnUpdate;
		streaming.Start();
	}

	private void OnUpdate(object? sender, StreamUpdateEventArgs e)
	{
		var tootId = e.Status.Id;
		var accountId = e.Status.Account.Id;
		var accountName = e.Status.Account.AccountName;
		var hasMedia = e.Status.MediaAttachments.Any();
		var hasAltText = e.Status.MediaAttachments.Any(x => !string.IsNullOrEmpty(x.Description));
		var createdAt = e.Status.CreatedAt;

		logger.LogInformation(
			"{tootId} de l'usuari {user} {temedia} {tealttext}",
			tootId,
			accountName,
			hasMedia ? "conté media" : "sense media",
			hasMedia && hasAltText ? " amb alt text" : hasMedia && !hasAltText ? " sense alt text" : ""
		);

		if (hasMedia)
		{
			var mediaToot = new MediaToot(accountId, accountName, tootId, hasAltText, createdAt);
			var args = new ListenerEngine.ListenerEventArgs(mediaToot);
			NewMediaToot?.Invoke(this, args);
		}
	}
}
