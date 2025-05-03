using EVideoGameStoreApp.Data.Base;
using EVideoGameStoreApp.Data.Enums;
using EVideoGameStoreApp.Data.ViewModels;
using EVideoGameStoreApp.Models;
using Microsoft.EntityFrameworkCore;


namespace EVideoGameStoreApp.Data.Services
{
    public class VideoGamesService : EntityBaseRepository<VideoGame>, IVideoGamesService
    {
        private readonly AppDbContext _context;
        public VideoGamesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<VideoGame> GetVideoGameByIdAsync(int id)
        {
            var videoGameDetails = await _context.VideoGames
                .Include(v => v.Developer)
                .Include(v => v.Publisher)
                .Include(v => v.VideoGamePlatforms)
                    .ThenInclude(vp => vp.Platform)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (videoGameDetails == null)
            {
                throw new InvalidOperationException($"VideoGame with ID {id} not found.");
            }

            return videoGameDetails;
        }

        public async Task<NewVideoGameDropdownsVM> GetNewVideoGameDropdownsAsync()
        {
            var response = new NewVideoGameDropdownsVM();
            {
                response.Developers = await _context.Developers.OrderBy(d => d.Name).ToListAsync();
                response.Platforms = await _context.Platforms.OrderBy(d => d.Name).ToListAsync();
                response.Publishers = await _context.Publishers.OrderBy(d => d.Name).ToListAsync();
            }
            return response;
        }

        public  async Task AddNewVideoGameAsync(NewVideoGameVM data)
        {
            var newVideoGame = new VideoGame()
            {
                Title = data.Title,
                Description = data.Description,
                Price = data.Price,
                CoverImageUrl = data.CoverImageUrl,
                ReleaseDate = data.ReleaseDate,
                PublisherId = data.PublisherId,
                DeveloperId = data.DeveloperId,
                VideoGameCategory = data.VideoGameCategories.Aggregate(VideoGameCategory.None, (current, category) => current | category)
            };
            await _context.VideoGames.AddAsync(newVideoGame);
            await _context.SaveChangesAsync();

            //Add VideoGamePlatforms
            foreach (var platformId in data.PlatformIds)
            {
                var newVideoGamePlatform = new VideoGamePlatform()
                {
                    VideoGameId = newVideoGame.Id,
                    PlatformId = platformId
                };
                await _context.VideoGamePlatforms.AddAsync(newVideoGamePlatform);
            }
            await _context.SaveChangesAsync();


        }

        public async Task UpdateVideoGameAsync(NewVideoGameVM data)
        {
            var dbVideoGame = await _context.VideoGames.FirstOrDefaultAsync(n => n.Id == data.Id);

            if(dbVideoGame != null)
            {

                dbVideoGame.Title = data.Title;
                   dbVideoGame.Description = data.Description;
                   dbVideoGame.Price = data.Price;
                   dbVideoGame.CoverImageUrl = data.CoverImageUrl;
                   dbVideoGame.ReleaseDate = data.ReleaseDate;
                   dbVideoGame.PublisherId = data.PublisherId;
                   dbVideoGame.DeveloperId = data.DeveloperId;
                dbVideoGame.VideoGameCategory = data.VideoGameCategories.Aggregate(VideoGameCategory.None, (current, category) => current | category);
                await _context.SaveChangesAsync();
            }

            // Remove existing platforms associated with the video game  
            var existingPlatformsDb = _context.VideoGamePlatforms.Where(n => n.VideoGameId == data.Id).ToList();
            _context.VideoGamePlatforms.RemoveRange(existingPlatformsDb);
            await _context.SaveChangesAsync();

            //Add VideoGamePlatforms
            foreach (var platformId in data.PlatformIds)
            {
                var newVideoGamePlatform = new VideoGamePlatform()
                {
                    VideoGameId = data.Id,
                    PlatformId = platformId
                };
                await _context.VideoGamePlatforms.AddAsync(newVideoGamePlatform);
            }
            await _context.SaveChangesAsync();
        }
    }
}
