namespace DataClasses;

public record MastodonParms
{
    public string? Instance { get; init; }
    public string? AccessToken { get; init; }
}
