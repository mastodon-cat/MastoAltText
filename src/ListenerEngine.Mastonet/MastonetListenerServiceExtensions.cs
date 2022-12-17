using ListenerEngine.Mastonet.Config;
using Microsoft.Extensions.DependencyInjection;

namespace ListenerEngine.Mastonet;

public static class MastonetListenerServiceExtensions
{
    public static IServiceCollection AddMastonetListener(
        this IServiceCollection serviceCollection, 
        Microsoft.Extensions.Configuration.IConfiguration configuration)
            =>
            serviceCollection
            .Configure<MastonetConfig>(configuration.GetSection(nameof(MastonetConfig)))
            .AddSingleton<IListener, MastonetListener>();
}