using DataClasses;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace StoreEngine.DbContext;

public class Store : IStore
{
    private readonly MastoAltTextDbContext dbContext;

    public Store(IDbContextFactory<MastoAltTextDbContext> contextFactory)
    {
        dbContext = contextFactory.CreateDbContext();
        Initialize();
    }

    public void Dispose()
    {
        dbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Creates the database and migrate if necessary.
    /// </summary>
    public async Task CreateDatabaseAndMigrateIfNecessary()
    {
        if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await dbContext.Database.MigrateAsync();
        }
    }

    public Task<List<MediaToot>> GetMediaTootsByUserIdAsync(string userId, int? year)
    {
        var query =
            dbContext
            .MediaToots
            .Where(m => m.AccountId == userId);

        if (year.HasValue)
            query = query.Where(m => m.CreatedAt.Year == year);

        return
        query
        .OrderBy(m => m.CreatedAt)
        .Select(m => m.AsData())
        .ToListAsync();
    }

    public Task<int> GetTootCountByUserIdAsync(string userId, bool? withDescription = null)
    {
        IQueryable<MediaTootModel> query = dbContext.MediaToots;
        if (withDescription.HasValue)
        {
            if (withDescription.Value)
            {
                query = query.Where(t => t.HasAltText);
            }
            else
            {
                query = query.Where(t => !t.HasAltText);
            }
        }

        return query.CountAsync(t => t.AccountId == userId);
    }

    public Task<int> GetNumberOfConsecutiveTootsWithDescriptionByUserIdAsync(string userId) =>
        GetNumberOfConsecutiveTootsByUserIdInternal(userId, true);


    public Task<int> GetNumberOfConsecutiveTootsWithoutDescriptionByUserIdAsync(string userId) =>
        GetNumberOfConsecutiveTootsByUserIdInternal(userId, false);

    private async Task<int> GetNumberOfConsecutiveTootsByUserIdInternal(string userId, bool withDescription)
    {
        var totalToots = await this.GetTootCountByUserIdAsync(userId);
        var cutPoint = await dbContext.MediaToots
            .Where(t => t.AccountId == userId && t.HasAltText == !withDescription)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefaultAsync();

        if (cutPoint == null)
        {
            return totalToots;
        }

        return totalToots - cutPoint.UserSequenceNumber;
    }


    public async Task SaveMediaTootAsync(MediaToot mediaToot)
    {
        var tootDb = mediaToot.AsModel();
        var lastUserToot = await dbContext.MediaToots
            .Where(t => t.AccountId == mediaToot.AccountId)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefaultAsync();
        tootDb.UserSequenceNumber = lastUserToot?.UserSequenceNumber ?? 1;
        await dbContext
            .MediaToots
            .AddAsync(tootDb);
        await dbContext
        .SaveChangesAsync();
    }

    public void Initialize()
    {
        var db =
            dbContext
            .Database;

        db
            .Migrate();
    }
}
