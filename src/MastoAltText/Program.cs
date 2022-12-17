using MastoAltText;
using ListenerEngine.Mastonet;
using System.Reflection;

IHost host =
	Host
	.CreateDefaultBuilder(args)
	.ConfigureAppConfiguration(hostConfig =>
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
	.ConfigureServices((hostBuilderContext, services) =>
	{
		services.AddHostedService<Worker>()
		// Dependency Injection
		.AddMastonetListener(hostBuilderContext.Configuration);
	})
	.Build();

await host.RunAsync();
