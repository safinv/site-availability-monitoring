using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using SiteAvailabilityMonitoring.Domain.Database.Contracts;
using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Models;

namespace SiteAvailabilityMonitoring.Controllers
{
    public class AccountController : BaseAuthController
    {
        private readonly IDbQuery<User> _userQuery;

        public AccountController(IDbQuery<User> userQuery)
        {
            _userQuery = userQuery ?? throw new ArgumentNullException(nameof(userQuery));
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
                var user = await _userQuery.GetAsync(u => u.Login == model.Login && u.Password == model.Password);
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
                var user = await _userQuery.GetAsync(u => u.Login == model.Login);
                if (user == null)
                {
                    await _userQuery.AddAsync(new User { Login = model.Login, Password = model.Password });

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