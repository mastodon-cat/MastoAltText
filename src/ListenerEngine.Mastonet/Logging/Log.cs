using Microsoft.Extensions.Logging;

namespace ListenerEngine.Mastonet.Logging;

internal static partial class Log
{

    [LoggerMessage(eventId: 0, level: LogLevel.Debug, message: "Connected as {accountName}")]
    public static partial void LogConnectedAs(this ILogger<MastonetListener> logger, string accountName);

    [LoggerMessage(
        eventId: 1,
        level: LogLevel.Information,
        message: "StatusId: [{tootId}] AccountName [{accountName}] AccountId: [{accountId}] " +
            "HasMedia: [{hasMedia}] HasAltText: [{hasAltText}]")]
    public static partial void LogTootReceived(this ILogger<MastonetListener> logger, string tootId, string accountName, string accountId, string hasMedia, string hasAltText);
}
