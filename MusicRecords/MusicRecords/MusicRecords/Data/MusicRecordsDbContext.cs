using Microsoft.EntityFrameworkCore;
using MusicRecords.Models;

namespace MusicRecords.Data;

public class MusicRecordsDbContext : DbContext
{
    public MusicRecordsDbContext(DbContextOptions<MusicRecordsDbContext> options) : base(options) { }

    public DbSet<MusicRecord> MusicRecords => Set<MusicRecord>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MusicRecord>(e =>
        {
            e.ToTable("MusicRecords");
            e.Property(r => r.Title).HasMaxLength(200).IsRequired();
            e.Property(r => r.Artist).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("Users");
            e.HasKey(u => u.Username);
            e.Property(u => u.Username).HasMaxLength(50);
        });
    }
}