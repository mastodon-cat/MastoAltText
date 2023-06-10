using DataClasses;
using Microsoft.Extensions.DependencyInjection;

namespace SenderEngine.Mastonet;

public static class MastonetSenderServiceExtensions
{
    public static IServiceCollection AddMastonetSender(
        this IServiceCollection serviceCollection,
        Microsoft.Extensions.Configuration.IConfiguration configuration)
            =>
            serviceCollection
            .Configure<MastodonParms>(configuration.GetSection(nameof(MastodonParms)))
            .AddSingleton<ISender, MastonetSender>();
}