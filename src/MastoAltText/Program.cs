using IntelligenceEngine.FirstVersion.Extensions;
using ListenerEngine.Mastonet;
using MastoAltText;
using StoreEngine.DbContext;
using System.Reflection;
using SenderEngine.Mastonet;

IHost host =
	Host
	.CreateDefaultBuilder(args)

	// configuration
	.ConfigureAppConfiguration( hostConfig =>

		hostConfig
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("mastodoncredentials.json", optional: true)
			.AddEnvironmentVariables(prefix: "MASTOALTTEXT_")	
			.AddUserSecrets(Assembly.GetExecutingAssembly(), true)
			.AddCommandLine(args)		

	)

	// services
	.ConfigureServices( (hostBuilderContext, services) =>

		services

			// The worker
			.AddHostedService<Worker>()

			// Dependencies
			.AddMastonetListener(hostBuilderContext.Configuration)
			.AddMastonetSender(hostBuilderContext.Configuration)
			.AddIntelligenceEngine(hostBuilderContext.Configuration)
			.AddStoreEngine(hostBuilderContext.Configuration)
	)

	// the build
	.Build();

await host.RunAsync();