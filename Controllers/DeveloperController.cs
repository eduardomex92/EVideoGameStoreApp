using EVideoGameStoreApp.Data;
using EVideoGameStoreApp.Data.Services;
using EVideoGameStoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EVideoGameStoreApp.Data.Static; // For UserRoles.Admin

namespace EVideoGameStoreApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class DeveloperController : Controller
    {
        private readonly IDevelopersService _service;

        public DeveloperController(IDevelopersService service)
        {
               _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View("Index", data);
        }

        //get: Developer/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Country, Description, LogoUrl")]Developer developer)
        {
            if (!ModelState.IsValid)
            {
                return View(developer); // Shows errors in the view
            }

            await _service.AddAsync(developer);
            return RedirectToAction(nameof(Index));
        }

        //Get: Developer/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var developerDetails = await _service.GetByIdAsync(id);
            if (developerDetails == null)
                return View("NotFound");

            return View(developerDetails);
        }

        //get: Developer/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var developerDetails = await _service.GetByIdAsync(id);
            if (developerDetails == null) return View("NotFound");
            return View(developerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Country, Description, LogoUrl")] Developer developer)
        {
            if (!ModelState.IsValid)
            {
                return View(developer); // Shows errors in the view
            }

            await _service.UpdateAsync(id, developer);
            return RedirectToAction(nameof(Index));
        }

        //get: Developer/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var developerDetails = await _service.GetByIdAsync(id);
            if (developerDetails == null) return View("NotFound");
            return View(developerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var developerDetails = await _service.GetByIdAsync(id);
            if (developerDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
