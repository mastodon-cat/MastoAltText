using Mastonet;
using Microsoft.Extensions.Logging;
using DataClasses;
using Microsoft.Extensions.Options;

namespace ListenerEngine.Mastonet;
public class MastonetListener : ListenerEngine.IListener
{
	private readonly MastodonParms config;
	private readonly MastodonClient client;
	private readonly ILogger<MastonetListener> logger;
	private TimelineStreaming? streaming = null;
	
	public MastonetListener(IOptions<MastodonParms> config, ILogger<MastonetListener> logger)
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
		CheckCredentials();

		streaming = client.GetPublicLocalStreaming();
		streaming.OnUpdate += OnUpdate;
		streaming.Start();
	}

    private void CheckCredentials()
    {
		var usr = client.GetCurrentUser().GetAwaiter().GetResult();			
		logger.LogDebug("Connectat com {}", usr.AccountName);
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
			$"StatusId: [{tootId}] AccountName [{accountName}] AccountId: [{accountId}] " +
			"HasMedia: [{hasMedia}] HasAltText: [{hasAltText}]",
			hasMedia ? "yes" : "no",
			hasMedia && hasAltText ? "yes" : hasMedia && !hasAltText ? "no" : ""
		);

		if (hasMedia)
		{
			var mediaToot = new MediaToot(null, accountId, accountName, tootId, hasAltText, createdAt);
			var args = new ListenerEngine.ListenerEventArgs(mediaToot);
			NewMediaToot?.Invoke(this, args);
		}
	}
}
