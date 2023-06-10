using DataClasses;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ListenerEngine.Mastonet;

public static class MastonetListenerServiceExtensions
{
    public static IServiceCollection AddMastonetListener(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
            =>
            serviceCollection
            .Configure<MastodonParms>(configuration.GetSection(nameof(MastodonParms)))
            .AddSingleton<IListener, MastonetListener>();
}