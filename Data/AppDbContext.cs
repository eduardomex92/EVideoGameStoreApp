using EVideoGameStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EVideoGameStoreApp.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoGamePlatform>().HasKey(vgp => new 
            { 
                vgp.VideoGameId, 
                vgp.PlatformId 
            });

            modelBuilder.Entity<VideoGamePlatform>()
                .HasOne(vgp => vgp.VideoGame)
                .WithMany(vg => vg.VideoGamePlatforms)
                .HasForeignKey(vgp => vgp.VideoGameId);

            modelBuilder.Entity<VideoGamePlatform>()
                .HasOne(vgp => vgp.Platform)
                .WithMany(p => p.VideoGamePlatforms)
                .HasForeignKey(vgp => vgp.PlatformId);

            // DeveloperPublisher join table
            modelBuilder.Entity<DeveloperPublisher>().HasKey(dp => new { dp.DeveloperId, dp.PublisherId });

            modelBuilder.Entity<DeveloperPublisher>()
                .HasOne(dp => dp.Developer)
                .WithMany(d => d.DeveloperPublishers)
                .HasForeignKey(dp => dp.DeveloperId);

            modelBuilder.Entity<DeveloperPublisher>()
                .HasOne(dp => dp.Publisher)
                .WithMany(p => p.DeveloperPublishers)
                .HasForeignKey(dp => dp.PublisherId);

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<VideoGame> VideoGames { get; set; }
        public DbSet<Developer> Developers { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Platform> Platforms { get; set; }

        public DbSet<VideoGamePlatform> VideoGamePlatforms { get; set; }
    }
}
