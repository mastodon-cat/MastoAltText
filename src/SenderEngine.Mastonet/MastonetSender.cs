using DataClasses;

using Mastonet;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SenderEngine.Mastonet;
public class MastonetSender : ISender
{
	private readonly MastodonParms config;
	private readonly MastodonClient client;
	private readonly ILogger<MastonetSender> logger;

	public MastonetSender(IOptions<MastodonParms> config, ILogger<MastonetSender> logger)
	{
		this.config = config.Value;
		this.logger = logger;
		client = new MastodonClient(
			this.config.Instance ?? throw new ArgumentNullException(nameof(this.config.Instance)),
			this.config.AccessToken ?? throw new ArgumentNullException(nameof(this.config.AccessToken))
			);
	}

	public void Dispose()
	{
		//         
	}

	// StatusId: [109535551891158730] AccountName [ctrl_alt_d] AccountId: [109378184178969113]
	public Task SendToot(string body, bool isPublic, string language)
		=>
		client.PublishStatus(
			status: body,
			visibility: isPublic ? Visibility.Public : Visibility.Direct,
			language: language
		);
}
