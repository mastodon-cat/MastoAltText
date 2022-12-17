namespace ListenerEngine.Mastonet.Config;

public record MastonetConfig
{
	public string? Instance { get; init; }
	public string? AccessToken { get; init; }
}
