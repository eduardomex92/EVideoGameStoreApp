using System.Diagnostics;
using EVideoGameStoreApp.Data.Enums;
using EVideoGameStoreApp.Data.Services;
using EVideoGameStoreApp.Data.Static;
using EVideoGameStoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EVideoGameStoreApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class VideoGamesController : Controller
    {
        private readonly IVideoGamesService _service;

        public VideoGamesController(IVideoGamesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allVideoGames = await _service.GetAllAsync(n => n.Publisher, n => n.Developer);
            return View("Index", allVideoGames);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allVideoGames = await _service.GetAllAsync(
                n => n.Publisher,
                n => n.Developer
            );

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                var filteredResult = allVideoGames.Where(n =>
                    (!string.IsNullOrEmpty(n.Title) && n.Title.ToLower().Contains(searchString)) ||
                    (!string.IsNullOrEmpty(n.Description) && n.Description.ToLower().Contains(searchString)) ||
                    (n.Publisher != null && n.Publisher.Name.ToLower().Contains(searchString)) ||
                    (n.Developer != null && n.Developer.Name.ToLower().Contains(searchString)) ||
                    (n.VideoGamePlatforms != null && n.VideoGamePlatforms.Any(p => p.Platform.Name.ToLower().Contains(searchString))) ||
                    Enum.GetValues(typeof(VideoGameCategory))
                        .Cast<VideoGameCategory>()
                        .Where(cat => cat != VideoGameCategory.None && n.VideoGameCategory.HasFlag(cat))
                        .Any(cat => cat.ToString().ToLower().Contains(searchString))
                ).ToList();

                return View("Index", filteredResult);
            }

            return View("Index", allVideoGames);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var videoGameDetail = await _service.GetVideoGameByIdAsync(id);
            if (videoGameDetail == null) return View("NotFound");
            return View(videoGameDetail);
        }

        public async Task<IActionResult> Create()
        {
            var videoGameDropdownsData = await _service.GetNewVideoGameDropdownsAsync();

            ViewBag.Publishers = new SelectList(videoGameDropdownsData.Publishers, "Id", "Name");
            ViewBag.Developers = new SelectList(videoGameDropdownsData.Developers, "Id", "Name");
            ViewBag.Platforms = new SelectList(videoGameDropdownsData.Platforms, "Id", "Name");

            ViewBag.CategoryList = Enum.GetValues(typeof(VideoGameCategory))
                .Cast<VideoGameCategory>()
                .Where(c => c != VideoGameCategory.None)
                .Select(c => new SelectListItem
                {
                    Value = ((int)c).ToString(),
                    Text = c.ToString()
                }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewVideoGameVM videoGameVM)
        {
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("ModelState is invalid");

                var dropDowns = await _service.GetNewVideoGameDropdownsAsync();
                ViewBag.Publishers = new SelectList(dropDowns.Publishers, "Id", "Name");
                ViewBag.Developers = new SelectList(dropDowns.Developers, "Id", "Name");
                ViewBag.Platforms = new SelectList(dropDowns.Platforms, "Id", "Name");

                ViewBag.CategoryList = Enum.GetValues(typeof(VideoGameCategory))
                    .Cast<VideoGameCategory>()
                    .Where(c => c != VideoGameCategory.None)
                    .Select(c => new SelectListItem
                    {
                        Value = ((int)c).ToString(),
                        Text = c.ToString()
                    }).ToList();

                return View(videoGameVM);
            }

            await _service.AddNewVideoGameAsync(videoGameVM);
            TempData["Success"] = "Videogame added successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var videoGameDetails = await _service.GetVideoGameByIdAsync(id);
            if (videoGameDetails == null) return View("NotFound");

            var response = new NewVideoGameVM
            {
                Id = videoGameDetails.Id,
                Title = videoGameDetails.Title,
                Description = videoGameDetails.Description,
                Price = videoGameDetails.Price,
                CoverImageUrl = videoGameDetails.CoverImageUrl,
                ReleaseDate = videoGameDetails.ReleaseDate,
                PublisherId = videoGameDetails.PublisherId,
                DeveloperId = videoGameDetails.DeveloperId,
                PlatformIds = videoGameDetails.VideoGamePlatforms.Select(n => n.PlatformId).ToList(),
                VideoGameCategories = Enum.GetValues(typeof(VideoGameCategory))
                    .Cast<VideoGameCategory>()
                    .Where(cat => videoGameDetails.VideoGameCategory.HasFlag(cat) && cat != VideoGameCategory.None)
                    .ToList()
            };

            var videoGameDropdownsData = await _service.GetNewVideoGameDropdownsAsync();

            ViewBag.Publishers = new SelectList(videoGameDropdownsData.Publishers, "Id", "Name");
            ViewBag.Developers = new SelectList(videoGameDropdownsData.Developers, "Id", "Name");
            ViewBag.Platforms = new SelectList(videoGameDropdownsData.Platforms, "Id", "Name");
            ViewBag.CategoryList = Enum.GetValues(typeof(VideoGameCategory))
                .Cast<VideoGameCategory>()
                .Where(c => c != VideoGameCategory.None)
                .Select(c => new SelectListItem
                {
                    Value = ((int)c).ToString(),
                    Text = c.ToString()
                }).ToList();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewVideoGameVM videoGameVM)
        {
            if (id != videoGameVM.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var dropDowns = await _service.GetNewVideoGameDropdownsAsync();
                ViewBag.Publishers = new SelectList(dropDowns.Publishers, "Id", "Name");
                ViewBag.Developers = new SelectList(dropDowns.Developers, "Id", "Name");
                ViewBag.Platforms = new SelectList(dropDowns.Platforms, "Id", "Name");

                ViewBag.CategoryList = Enum.GetValues(typeof(VideoGameCategory))
                    .Cast<VideoGameCategory>()
                    .Where(c => c != VideoGameCategory.None)
                    .Select(c => new SelectListItem
                    {
                        Value = ((int)c).ToString(),
                        Text = c.ToString()
                    }).ToList();

                return View(videoGameVM);
            }

            await _service.UpdateVideoGameAsync(videoGameVM);
            TempData["Success"] = "Videogame edited successfully!";

            return RedirectToAction(nameof(Index));
        }
    }
}
