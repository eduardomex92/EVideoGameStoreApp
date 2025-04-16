using EVideoGameStoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EVideoGameStoreApp.Controllers
{
    public class VideoGamesController : Controller
    {
        private readonly AppDbContext _context;

        public VideoGamesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allVideoGames = await _context.VideoGames
                .Include(v => v.Developer)
                .Include(v => v.Publisher)
                .ToListAsync();
            return View("Index", allVideoGames);
        }
    }
}
