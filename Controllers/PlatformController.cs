using EVideoGameStoreApp.Data.Services;
using EVideoGameStoreApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EVideoGameStoreApp.Controllers
{
    public class PlatformController : Controller
    {
        private readonly IPlatformsService _service;

        public PlatformController(IPlatformsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var platforms = await _service.GetAllAsync();
            return View(platforms);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Description, LogoUrl")] Platform platform)
        {
            if (!ModelState.IsValid) return View(platform);

            await _service.AddAsync(platform);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var platformDetails = await _service.GetByIdAsync(id);
            if (platformDetails == null) return View("NotFound");

            return View(platformDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var platformDetails = await _service.GetByIdAsync(id);
            if (platformDetails == null) return View("NotFound");

            return View(platformDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("PlatformId, Name, Description, LogoUrl")] Platform platform)
        {
            if (!ModelState.IsValid) return View(platform);

            await _service.UpdateAsync(id, platform);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var platformDetails = await _service.GetByIdAsync(id);
            if (platformDetails == null) return View("NotFound");

            return View(platformDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
