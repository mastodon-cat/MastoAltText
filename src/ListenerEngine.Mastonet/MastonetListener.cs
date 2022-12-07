﻿using Mastonet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DataClasses;

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
        var accountId = e.Status.Account.Id;
        var accountName = e.Status.Account.AccountName;
        var hasMedia = e.Status.MediaAttachments.Any();
        var hasAltText = e.Status.MediaAttachments.Any(x=>!string.IsNullOrEmpty(x.Description));
        var createdAt = e.Status.CreatedAt;

        Logger.LogInformation(
            "{tootId} de l'usuari {user} {temedia} {tealttext}",
            tootId,
            accountName,
            hasMedia?"conté media":"sense media",
            hasMedia&&hasAltText?" amb alt text":hasMedia&&!hasAltText?" sense alt text":""
        );

        if (hasMedia)
        {
            var mediaToot = new MediaToot(accountId, accountName, tootId, hasAltText, createdAt);
            var args = new ListenerEngine.ListenerEventArgs(mediaToot);
            NewMediaToot?.Invoke(this, args);
        }
    }

}
