namespace StoreEngine.DbContext;

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class BloggingContextFactory : IDesignTimeDbContextFactory<MastoAltTextDbContext>
{
    public MastoAltTextDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", true)
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
            .Build();
            
        var optionsBuilder = 
            new
            DbContextOptionsBuilder<MastoAltTextDbContext>()
            // .UseNpgsql(configuration.GetConnectionString("MastonetDbContextDatabase"));
            .UseNpgsql("Fake connection string");
        return new MastoAltTextDbContext(optionsBuilder.Options);
    }
}