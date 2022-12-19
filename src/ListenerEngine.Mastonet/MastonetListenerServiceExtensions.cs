using DataClasses;
using Microsoft.Extensions.DependencyInjection;

namespace ListenerEngine.Mastonet;

public static class MastonetListenerServiceExtensions
{
    public static IServiceCollection AddMastonetListener(
        this IServiceCollection serviceCollection, 
        Microsoft.Extensions.Configuration.IConfiguration configuration)
            =>
            serviceCollection
            .Configure<MastodonParms>(configuration.GetSection(nameof(MastodonParms)))
            .AddSingleton<IListener, MastonetListener>();
}