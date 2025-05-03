using EVideoGameStoreApp.Data;
using EVideoGameStoreApp.Data.Static;
using EVideoGameStoreApp.Data.ViewModels;
using EVideoGameStoreApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace EVideoGameStoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
        public IActionResult Login(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                TempData["Error"] = "Please log in to continue.";
            }

            return View(new LoginVM());
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        TempData["Success"] = "Welcome back, " + user.FullName + "!";
                        return RedirectToAction("Index", "VideoGames");
                    }

                }
                TempData["Error"] = "Invalid login attempt. Please try again.";
                return View(loginVM);

            }
            //if login fails, return to login page
            TempData["Error"] = "Invalid login attempt. Please try again.";
            return View(loginVM);
        }



        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This Email is already in use. Please try again.";
                return View(registerVM);
            }
            //if user is not found, create a new user
            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,

            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");
        }

        [HttpPost]

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Clear the shopping cart from the session
            var cartId = HttpContext.Session.GetString("CartId");
            if (!string.IsNullOrEmpty(cartId))
            {
                var shoppingCartItems = _context.ShoppingCartItems.Where(s => s.ShoppingCartId == cartId);
                _context.ShoppingCartItems.RemoveRange(shoppingCartItems);
                await _context.SaveChangesAsync();
            }

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "VideoGames");
        }


        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}
