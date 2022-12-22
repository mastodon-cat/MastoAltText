using IntelligenceEngine.FirstVersion.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligenceEngine.FirstVersion.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddIntelligenceEngine(
		this IServiceCollection services, 
		IConfiguration configuration)
		=>
		services
			.Configure<List<AppMessage>>(configuration.GetSection("AppMessages"))
			.AddSingleton<IIntelligence, Intelligence>();
}
