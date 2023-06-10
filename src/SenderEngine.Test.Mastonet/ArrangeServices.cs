using System.Reflection;
using DataClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SenderEngine.Mastonet;

namespace SenderEngine.Test.Mastonet;

public static class ArrangeServices
{
    public static void RealServices(this IServiceCollection services)
    {
        var config =
            new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetExecutingAssembly(), false)
            .Build();

        services
        .Configure<MastodonParms>(config.GetSection(nameof(MastodonParms)))
        .AddSingleton<ISender, MastonetSender>();
    }

    public static void MockServices(this IServiceCollection services)
    {
        services
            .AddSingleton(Mock.Of<ISender>());
    }
}