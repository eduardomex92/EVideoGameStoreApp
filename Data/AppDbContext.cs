using EVideoGameStoreApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EVideoGameStoreApp.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // VideoGame <-> Platform many-to-many
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

            // Developer <-> Publisher many-to-many
            modelBuilder.Entity<DeveloperPublisher>().HasKey(dp => new
            {
                dp.DeveloperId,
                dp.PublisherId
            });

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

        // Entity tables
        public DbSet<VideoGame> VideoGames { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Platform> Platforms { get; set; }

        // Join tables
        public DbSet<VideoGamePlatform> VideoGamePlatforms { get; set; }
        public DbSet<DeveloperPublisher> DeveloperPublisher { get; set; }

        // Orders and cart
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
