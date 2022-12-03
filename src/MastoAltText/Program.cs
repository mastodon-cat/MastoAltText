using MastoAltText;
using ListenerEngine;
using ListenerEngine.Mastonet;

IHost host = 
    Host
    .CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(hostConfig =>
        {
            hostConfig.SetBasePath(Directory.GetCurrentDirectory());
            hostConfig.AddJsonFile("mastodoncredentials.json", optional: true);
            hostConfig.AddEnvironmentVariables(prefix: "MASTOALTTEXT_");
            hostConfig.AddCommandLine(args);
        }
    )
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();        
        services.AddSingleton<IListener, MastonetListener>();
    })
    .Build();

await host.RunAsync();
