using Microsoft.Extensions.DependencyInjection;

namespace ListenerEngine.Mastonet;

public static class MastonetListenerServiceExtensions
{
    public static IServiceCollection AddMastonetListener(this IServiceCollection serviceCollection)
        =>
        serviceCollection
        .AddSingleton<IListener, MastonetListener>();
}