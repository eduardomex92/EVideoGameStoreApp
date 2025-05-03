using EVideoGameStoreApp.Models;
using EVideoGameStoreApp.Data.Base;
using EVideoGameStoreApp.Data.ViewModels;

namespace EVideoGameStoreApp.Data.Services
{
    public interface IVideoGamesService : IEntityBaseRepository<VideoGame>
    {
        Task<VideoGame> GetVideoGameByIdAsync(int id);
        Task<NewVideoGameDropdownsVM> GetNewVideoGameDropdownsAsync();

        Task AddNewVideoGameAsync(NewVideoGameVM data);
        Task UpdateVideoGameAsync(NewVideoGameVM data);
    }
}
