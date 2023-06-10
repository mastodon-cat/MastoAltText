using DataClasses;

using MastoAltText.Common.Exceptions;

using Mastonet;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SenderEngine.Mastonet;
public class MastonetSender : ISender
{
	private readonly MastodonParms config;
	private readonly MastodonClient client;

	public MastonetSender(IOptions<MastodonParms> config)
	{
		if (config is null)
		{
			throw new ArgumentNullException(nameof(config));
		}

		this.config = config.Value;
		client = new MastodonClient(
			this.config.Instance ?? throw new PropertyNullException("Instance cannot be null.", nameof(this.config.Instance)),
			this.config.AccessToken ?? throw new PropertyNullException("Config.AccessToken cannot be null.", nameof(this.config.AccessToken))
			);
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	// StatusId: [109535551891158730] AccountName [ctrl_alt_d] AccountId: [109378184178969113]
	public Task SendToot(string body, bool isPublicToot, string language)
		=>
		client.PublishStatus(
			status: body,
			visibility: isPublicToot ? Visibility.Public : Visibility.Direct,
			language: language
		);
}
