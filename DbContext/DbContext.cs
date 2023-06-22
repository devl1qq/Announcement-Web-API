using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Announcement_Web_API.Entities;

public class AnnouncementDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AnnouncementDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Announcement> Announcements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = _configuration.GetConnectionString("AnnouncementDbConnection");

        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>().ToTable("Announcements");

        base.OnModelCreating(modelBuilder);
    }

    public void EnsureDatabaseCreated()
    {
        Database.Migrate();
    }
}
