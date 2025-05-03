using System.Security.Claims;
using EVideoGameStoreApp.Data.Cart;
using EVideoGameStoreApp.Data.Services;
using EVideoGameStoreApp.Data.Static;
using EVideoGameStoreApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EVideoGameStoreApp.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IVideoGamesService _videoGamesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IVideoGamesService videoGamesService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _videoGamesService = videoGamesService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }

        [AllowAnonymous]
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        [AllowAnonymous]
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _videoGamesService.GetVideoGameByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.AddItemtoCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        [AllowAnonymous]
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _videoGamesService.GetVideoGameByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Error"] = "You must be logged in to complete your order.";
                return RedirectToAction("Login", "Account");
            }

            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }

    }
}
