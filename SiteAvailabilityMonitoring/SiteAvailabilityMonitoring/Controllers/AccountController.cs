using Microsoft.AspNetCore.Mvc;
using System;
using SiteAvailabilityMonitoring.Models;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Providers;

namespace SiteAvailabilityMonitoring.Controllers
{
    public class AccountController : BaseAuthController
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.TryGetUser(model.Login, model.Password);
                if (user != null)
                {
                    await Authenticate(model.Login);

                    return RedirectToAction("Index", "Administration");
                }

                ModelState.AddModelError("", "Incorrect login or email");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.TryGetUser(model.Login);
                if (user == null)
                {
                    await _userService.CreateAsync(new UserModel { Login = model.Login, Password = model.Password});

                    await Authenticate(model.Login);

                    return RedirectToAction("Index", "Administration");
                }
                else
                    ModelState.AddModelError("", "Incorrect login or email");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
