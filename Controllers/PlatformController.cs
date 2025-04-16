using EVideoGameStoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EVideoGameStoreApp.Controllers
{
    public class PlatformController : Controller
    {
        private readonly AppDbContext _context;

        public PlatformController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allPlatforms = await _context.Platforms.ToListAsync();
            return View("Index", allPlatforms);
        }
    }
}
