using Mastonet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ListenerEngine.Mastonet;
public class MastonetListener: ListenerEngine.IListener, IDisposable
{
    private readonly MastodonClient Client;
    private readonly TimelineStreaming Streaming;
    private readonly ILogger<MastonetListener> Logger;
    public MastonetListener(IConfiguration configuration, ILogger<MastonetListener> logger)
    {
        Logger = logger;

        var instance = 
            configuration
            .GetValue<string>("Instance")
            ?? throw new ArgumentException("Cal passar paràmetre: Instance");
        var accessToken = 
            configuration
            .GetValue<string>("AccessToken")
            ?? throw new ArgumentException("Cal passar paràmetre: AccessToken");

        Client = new MastodonClient(instance, accessToken);

        Streaming = Client.GetPublicLocalStreaming();
        Streaming.OnUpdate += OnUpdate;
    }

    public event EventHandler<ListenerEventArgs>? NewMediaToot;

    public void Dispose()
    {
        Streaming.OnUpdate -= OnUpdate;
    }

    public void Start()
    {        
        Streaming.Start();
    }

    public void Stop()
    {
        Streaming.Stop();
    }

    private void OnUpdate(object? sender, StreamUpdateEventArgs e)
    {
        var tootId = e.Status.Id;
        var user = e.Status.Account.AccountName;
        var hasMedia = e.Status.MediaAttachments.Any();
        var hasAltText = e.Status.MediaAttachments.Any(x=>!string.IsNullOrEmpty(x.Description));

        Logger.LogInformation(
            "{tootId} de l'usuari {user} {temedia} {tealttext}",
            tootId,
            user,
            hasMedia?"conté media":"sense media",
            hasMedia&&hasAltText?" amb alt text":hasMedia&&!hasAltText?" sense alt text":""
        );

        if (hasMedia)
        {
            var args = new ListenerEngine.ListenerEventArgs(user, tootId, hasAltText);
            NewMediaToot?.Invoke(this, args);
        }
    }

}
