using EVideoGameStoreApp.Data.Services;
using EVideoGameStoreApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EVideoGameStoreApp.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublishersService _service;

        public PublisherController(IPublishersService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var publishers = await _service.GetAllAsync();
            return View(publishers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Country, LogoUrl")] Publisher publisher)
        {
            if (!ModelState.IsValid) return View(publisher);

            await _service.AddAsync(publisher);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var publisherDetails = await _service.GetByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");

            return View(publisherDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var publisherDetails = await _service.GetByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");

            return View(publisherDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("PublisherId, Name, Country, LogoUrl")] Publisher publisher)
        {
            if (!ModelState.IsValid) return View(publisher);

            await _service.UpdateAsync(id, publisher);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var publisherDetails = await _service.GetByIdAsync(id);
            if (publisherDetails == null) return View("NotFound");

            return View(publisherDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
