using EWebApp.Data.Static;
using EWebApp.Dbclass;
using EWebApp.Models;
using EWebApp.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 AppDbContext context)
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

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginVN();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVN loginVN)
        {
            if(!ModelState.IsValid) return View(loginVN);

            var user = await _userManager.FindByEmailAsync(loginVN.EmailAddress);
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVN.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVN.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("index", "Production");
                    }
                }

                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginVN);
            }

            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginVN);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterVN();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVN registerVN)
        {
            if(!ModelState.IsValid) return View(registerVN);

            var user = await _userManager.FindByEmailAsync(registerVN.EmailAddress);

            if(user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVN);
            }
            var newUser = new ApplicationUser()
            {
                FullName = registerVN.FullName,
                Email = registerVN.EmailAddress,
                UserName = registerVN.EmailAddress

            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVN.Password);
            if(newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }

            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Production");
        }
    }
}
