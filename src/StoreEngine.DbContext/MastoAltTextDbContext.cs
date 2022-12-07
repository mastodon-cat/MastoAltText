namespace StoreEngine.DbContext;

using Microsoft.EntityFrameworkCore;

public class MastoAltTextDbContext: DbContext
{    
    public MastoAltTextDbContext(DbContextOptions<MastoAltTextDbContext> options)
        : base(options)
    {
    }

    public DbSet<MediaTootModel> MediaToots {get; set; }= default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // pk
        modelBuilder.Entity<MediaTootModel>().HasKey(m => m.Id);

        // MaxLength
        modelBuilder.Entity<MediaTootModel>().Property(m => m.TootId).HasMaxLength(50);
        modelBuilder.Entity<MediaTootModel>().Property(m => m.AccountId).HasMaxLength(50);
        modelBuilder.Entity<MediaTootModel>().Property(m => m.AccountName).HasMaxLength(250);

        // index
        modelBuilder.Entity<MediaTootModel>().HasIndex(m => new {m.AccountId, m.CreatedAt});
        modelBuilder.Entity<MediaTootModel>().HasIndex(m => m.CreatedAt);
        modelBuilder.Entity<MediaTootModel>().HasIndex(m => m.TootId).IsUnique();
    }
}