using MastoAltText;
using ListenerEngine.Mastonet;
using System.Reflection;
using ListenerEngine.Mastonet.Config;


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
		services.Configure<MastonetConfig>(hostBuilderContext.Configuration.GetSection(nameof(MastonetConfig)));
		services.AddHostedService<Worker>()
		// Dependency Injection
		.AddMastonetListener();
	})
	.Build();

await host.RunAsync();
