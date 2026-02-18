using Microsoft.EntityFrameworkCore;
using Shorty.Api.Data.Entities;

namespace Shorty.Api.Data;

public sealed class AppDbContext : DbContext {
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
   
   public DbSet<User> Users => Set<User>();
   public DbSet<Link> Links => Set<Link>();
   public DbSet<Click> Clicks => Set<Click>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>(b =>
        {
            b.Property(x => x.Name).HasMaxLength(100);
            b.Property(x => x.ApiKeyHash).HasMaxLength(200);
        });

        modelBuilder.Entity<Link>(b =>
        {
            b.Property(x => x.Slug).HasMaxLength(64);
            b.HasIndex(x => x.Slug).IsUnique();
            
            b.Property(x => x.Url).HasMaxLength(2048);
            b.HasOne(x => x.User)
                .WithMany(u => u.Links)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // composite index for filter performance
            b.HasIndex(x => new { x.UserId, x.createdAt });
        });
        
        modelBuilder.Entity<Click>(b =>
        {
            b.HasIndex(x => new { x.ClickedAt, x.LinkId });

            b.Property(x => x.UserAgent).HasMaxLength(512);
            b.Property(x => x.Referer).HasMaxLength(2048);

            b.HasOne(x => x.Link)
                .WithMany(l => l.Clicks)
                .HasForeignKey(x => x.LinkId)
                .OnDelete(DeleteBehavior.Cascade); // not keeping track of clicks for dead links
        });
    }
}