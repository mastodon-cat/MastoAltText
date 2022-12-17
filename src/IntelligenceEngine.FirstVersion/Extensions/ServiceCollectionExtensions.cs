using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IntelligenceEngine.FirstVersion.Extensions;

public static class ServiceCollectionExtensions
{

	public static IServiceCollection AddIntelligenceEngine(this IServiceCollection services)
	{
		services.AddSingleton<IIntelligence, Intelligence>();
		return services;
	}
}
