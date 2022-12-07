
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace StoreEngine.DbContext;

public static class MastonetListenerServiceExtensions
{
    public static IServiceCollection ConfigureServices(IServiceCollection services)
        =>
        services
        .AddDbContextFactory<MastoAltTextDbContext>(
            options =>
                options.UseSqlite(@"Server=(localdb)\mssqllocaldb;Database=Test")
        )
        .AddSingleton<IStore, Store>();
    
}