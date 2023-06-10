using IntelligenceEngine.DynamicConditions.Extensions;
using ListenerEngine.Mastonet;
using MastoAltText;
using StoreEngine.DbContext;
using System.Reflection;
using SenderEngine.Mastonet;

IHost host =
    Host
    .CreateDefaultBuilder(args)
    // configuration
    .ConfigureAppConfiguration(hostConfig =>
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        if (string.IsNullOrWhiteSpace(environment))
        {
            environment = "Production";
        }

        hostConfig
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: false)
            .AddEnvironmentVariables(prefix: "MASTOALTTEXT_")
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
            .AddCommandLine(args);
    })
    // services
    .ConfigureServices((hostBuilderContext, services) =>
    {
        services
            // The worker
            .AddHostedService<Worker>()
            // Dependencies
            .AddMastonetListener(hostBuilderContext.Configuration)
            .AddMastonetSender(hostBuilderContext.Configuration)
            .AddIntelligenceEngine(hostBuilderContext.Configuration)
            .AddStoreEngine(hostBuilderContext.Configuration);
    })
    // the build
    .Build();

await host.RunAsync();