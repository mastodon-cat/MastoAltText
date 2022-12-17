using MastoAltText;
using ListenerEngine.Mastonet;
using System.Reflection;

IHost host = 
    Host
    .CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(hostConfig =>
        {
            // to get access token and mastodon instance from somewhere:
            // json, env vars or command line arguments.
            hostConfig.SetBasePath(Directory.GetCurrentDirectory());
            hostConfig.AddJsonFile("mastodoncredentials.json", optional: true);
            hostConfig.AddEnvironmentVariables(prefix: "MASTOALTTEXT_");
            hostConfig.AddCommandLine(args);
            hostConfig.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
        }
    )
    .ConfigureServices(services =>
    {
        services
        .AddHostedService<Worker>()
        // Dependency Injection
        .AddMastonetListener();
    })
    .Build();

await host.RunAsync();
