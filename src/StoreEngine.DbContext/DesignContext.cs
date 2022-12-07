namespace StoreEngine.DbContext;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class BloggingContextFactory : IDesignTimeDbContextFactory<MastoAltTextDbContext>
{
    public MastoAltTextDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = 
            new
            DbContextOptionsBuilder<MastoAltTextDbContext>()
            .UseSqlite("Fake connection string");
        return new MastoAltTextDbContext(optionsBuilder.Options);
    }
}