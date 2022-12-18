using IntelligenceEngine.FirstVersion.Entities;
using IntelligenceEngine.FirstVersion.Extensions;
using ListenerEngine.Mastonet;
using MastoAltText;
using StoreEngine.DbContext;
using System.Reflection;
using SenderEngine.Mastonet;

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
		services
			.Configure<List<AppMessage>>(hostBuilderContext.Configuration.GetSection("AppMessages"))
			.AddHostedService<Worker>()
			// Dependency Injection
			.AddMastonetListener(hostBuilderContext.Configuration)
			.AddMastonetSender(hostBuilderContext.Configuration)
			.AddIntelligenceEngine()
			.AddStoreEngine(hostBuilderContext.Configuration);
	})
	.Build();

await host.RunAsync();