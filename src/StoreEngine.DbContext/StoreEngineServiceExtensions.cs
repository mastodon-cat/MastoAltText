
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StoreEngine.DbContext;

public static class MastonetListenerServiceExtensions
{
    public static IServiceCollection AddStoreEngine(
        this IServiceCollection services, 
        IConfiguration configuration)
        =>
        services
        .AddDbContextFactory<MastoAltTextDbContext>(
            options =>
                options.UseNpgsql(configuration.GetConnectionString("MastonetDbContextDatabase"))
        )
        .AddSingleton<IStore, Store>();
    
}