using Microsoft.Extensions.DependencyInjection;

namespace IntelligenceEngine.FirstVersion.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddIntelligenceEngine(this IServiceCollection services)
	{
		services.AddSingleton<IIntelligence, Intelligence>();
		return services;
	}
}
