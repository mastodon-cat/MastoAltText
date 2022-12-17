using IntelligenceEngine.FirstVersion.Entities;
using IntelligenceEngine.FirstVersion.Extensions;

using ListenerEngine.Mastonet;
using ListenerEngine.Mastonet.Config;

using MastoAltText;

using Microsoft.EntityFrameworkCore;

using StoreEngine;
using StoreEngine.DbContext;

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
		services.Configure<List<AppMessage>>(hostBuilderContext.Configuration.GetSection("AppMessages"));
		services.Configure<MastonetConfig>(hostBuilderContext.Configuration.GetSection(nameof(MastonetConfig)));
		services.AddHostedService<Worker>()
		// Dependency Injection
		.AddMastonetListener()
		.AddIntelligenceEngine()
		.AddDbContextFactory< MastoAltTextDbContext>(builder =>
		{
			builder.UseSqlite(hostBuilderContext.Configuration.GetConnectionString("MastonetDbContextDatabase"));
		})
		.AddSingleton<IStore, Store>();
	})
	.Build();

await host.RunAsync();
