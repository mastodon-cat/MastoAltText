using DataClasses;
using Microsoft.EntityFrameworkCore;

namespace StoreEngine.DbContext;

public class Store : IStore
{
    private readonly IDbContextFactory<MastoAltTextDbContext> ContextFactory;

    public Store(IDbContextFactory<MastoAltTextDbContext> contextFactory)
    {
        ContextFactory = contextFactory;
        Initialize();
    }

    public void Dispose()
    {
    }

    public async Task<List<MediaToot>> GetMediaTootsByUserIdAsync(string userId, int? year)
    {
        using var ctx = 
            await 
            ContextFactory
            .CreateDbContextAsync();

        var query =
            ctx
            .MediaToots
            .Where(m => m.AccountId == userId);
        
        if (year.HasValue)
            query = query.Where(m => m.CreatedAt.Year == year);

        var result =            
            await
            query
            .OrderBy(m=>m.CreatedAt)
            .ToListAsync();        

        return
            result
            .Select(m => m.AsData())
            .ToList();
    }


    public async Task SaveMediaTootAsync(MediaToot mediaToot)
    {
        using var ctx = 
            await 
            ContextFactory
            .CreateDbContextAsync();
        
        ctx
            .MediaToots
            .Add(
                mediaToot.AsModel()
            );

        await 
            ctx
            .SaveChangesAsync();
    }

    public void Initialize()
    {
        using var ctx = 
            ContextFactory
            .CreateDbContext();

        var db =
            ctx
            .Database;

        db
            .Migrate();
    }

}
