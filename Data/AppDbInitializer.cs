using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using EVideoGameStoreApp.Data;
using EVideoGameStoreApp.Models;
using EVideoGameStoreApp.Data.Enums;


public static class AppDbInitializer
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            context.Database.EnsureCreated();

            //Platforms
            if (!context.Platforms.Any())
            {
                context.Platforms.AddRange(new List<Platform>()
                {
                    new Platform() 
                    { 
                        Name = "PlayStation 5", 
                        Description = "Sony's latest gaming console", 
                        Manufacturer = "Sony" 
                    },
                    new Platform() 
                    { 
                        Name = "Xbox Series X", 
                        Description = "Microsoft's latest gaming console", 
                        Manufacturer = "Microsoft" 
                    },
                    new Platform() 
                    { 
                        Name = "Nintendo Switch", 
                        Description = "Nintendo's hybrid console", 
                        Manufacturer = "Nintendo" 
                    },
                    new Platform() 
                    {
                        Name = "PC (Steam)", 
                        Description = "PC gaming platform by Valve",
                        Manufacturer = "Valve" 
                    }
                });
                context.SaveChanges();
            }
            //Publisher
            if (!context.Publishers.Any())
            {
                context.Publishers.AddRange(new List<Publisher>()
                {
                    new Publisher() 
                    {
                        Name = "Nintendo",
                        Headquarters = "Kyoto, Japan",
                        LogoUrl = "/images/publishers/nintendo.png" 
                    },
                    new Publisher() 
                    {
                        Name = "Sony Interactive Entertainment", 
                        Headquarters = "Tokyo, Japan",
                        LogoUrl = "/images/publishers/sony.png" 
                    },
                    new Publisher() 
                    { 
                        Name = "Microsoft Studios",
                        Headquarters = "Redmond, WA", 
                        LogoUrl = "/images/publishers/microsoft.png"
                    }
                });
                context.SaveChanges();
            }
            //Developers
            if (!context.Developers.Any())
            {
                context.Developers.AddRange(new List<Developer>()
                {
                    new Developer() 
                    {
                        Name = "Nintendo EPD",
                        Country = "Japan",
                        LogoUrl = "/images/developers/nintendoepd.jpg", 
                        Description = "Development team for flagship Nintendo titles."
                    },
                    new Developer() 
                    { 
                        Name = "Guerrilla Games", 
                        Country = "Netherlands",
                        LogoUrl = "/images/developers/guerrilla.png", 
                        Description = "Developer of Horizon series."
                    },
                    new Developer() 
                    {
                        Name = "343 Industries", 
                        Country = "USA", 
                        LogoUrl = "/images/developers/343.png", 
                        Description = "Creators of the Halo franchise." 
                    }
                });
                context.SaveChanges();
            }
            //VideoGames
            if (!context.VideoGames.Any())
            {
                var nintendoDev = context.Developers.FirstOrDefault(d => d.Name == "Nintendo EPD");
                var sonyDev = context.Developers.FirstOrDefault(d => d.Name == "Guerrilla Games");
                var microsoftDev = context.Developers.FirstOrDefault(d => d.Name == "343 Industries");

                var nintendoPub = context.Publishers.FirstOrDefault(p => p.Name == "Nintendo");
                var sonyPub = context.Publishers.FirstOrDefault(p => p.Name == "Sony Interactive Entertainment");
                var microsoftPub = context.Publishers.FirstOrDefault(p => p.Name == "Microsoft Studios");

                context.VideoGames.AddRange(new List<VideoGame>()
                {
                    new VideoGame() 
                    {
                        Title = "The Legend of Zelda: Tears of the Kingdom",
                        ReleaseDate = DateTime.Now,
                        Price = 59.99m,
                        Description = "Explore Hyrule in an epic adventure.",
                        CoverImageUrl = "/images/games/zelda-totk.jpg",
                        DeveloperId = nintendoDev.Id,
                        PublisherId = nintendoPub.Id,
                        VideoGameCategory = VideoGameCategory.Adventure
                    },
                    new VideoGame() 
                    {
                        Title = "Horizon Forbidden West",
                        ReleaseDate = DateTime.Now,
                        Price = 69.99m,
                        Description = "Aloy's new journey in a post-apocalyptic world.",
                        CoverImageUrl = "/images/games/horizon-fw.jpg",
                        DeveloperId = sonyDev.Id,
                        PublisherId = sonyPub.Id,
                        VideoGameCategory = VideoGameCategory.Action
                    },
                    new VideoGame() 
                    {
                        Title = "Halo Infinite",
                        ReleaseDate = DateTime.Now,
                        Price = 59.99m,
                        Description = "Master Chief's latest chapter.",
                        CoverImageUrl = "/images/games/halo-infinite.jpg",
                        DeveloperId = microsoftDev.Id,
                        PublisherId = microsoftPub.Id,
                        VideoGameCategory = VideoGameCategory.Shooter
                    }
                });
                context.SaveChanges();
            }
            //VideoGamePlatforms
            if (!context.VideoGamePlatforms.Any())
            {
                var zelda = context.VideoGames.FirstOrDefault(v => v.Title == "The Legend of Zelda: Tears of the Kingdom");
                var horizon = context.VideoGames.FirstOrDefault(v => v.Title == "Horizon Forbidden West");
                var halo = context.VideoGames.FirstOrDefault(v => v.Title == "Halo Infinite");

                var ps5 = context.Platforms.FirstOrDefault(p => p.Name == "PlayStation 5");
                var xbox = context.Platforms.FirstOrDefault(p => p.Name == "Xbox Series X");
                var Nswitch = context.Platforms.FirstOrDefault(p => p.Name == "Nintendo Switch");
                var steam = context.Platforms.FirstOrDefault(p => p.Name.Contains("Steam"));

                context.VideoGamePlatforms.AddRange(new List<VideoGamePlatform>()
                {
                    new VideoGamePlatform()
                    {
                        VideoGameId = zelda.Id,
                        PlatformId = Nswitch.Id
                    },
                    new VideoGamePlatform()
                    {
                        VideoGameId = horizon.Id,
                        PlatformId = ps5.Id
                    },
                    new VideoGamePlatform()
                    {
                        VideoGameId = halo.Id,
                        PlatformId = xbox.Id
                    },
                    new VideoGamePlatform()
                    {
                        VideoGameId = halo.Id,
                        PlatformId = steam.Id
                    }
                });
                context.SaveChanges();

            }
        }

    }
}