using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Infrastructure.Repositories;
using SiteAvailabilityMonitoring.Models;

namespace SiteAvailabilityMonitoring.Controllers
{
    public class AccountController : BaseAuthController
    {
        private readonly UserRepository _userRepository;

        public AccountController(UserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException();
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
                var user = await _userRepository.GetAsync(u => u.Login == model.Login && u.Password == model.Password);
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
                var user = await _userRepository.GetAsync(u => u.Login == model.Login);
                if (user == null)
                {
                    await _userRepository.AddAsync(new User { Login = model.Login, Password = model.Password });

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