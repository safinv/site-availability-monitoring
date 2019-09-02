using Domain.Models;
using Infrastructure.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteAvailabilityMonitoring.Models;
using System;
using System.Threading.Tasks;

namespace SiteAvailabilityMonitoring.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        private readonly UrlService _urlService;
        private readonly BackgroundSettingService _backgroundSettingService;

        private const string IndexActionName = "Index";

        public AdministrationController(UrlService urlService, BackgroundSettingService backgroundSettingService)
        {
            _urlService = urlService ?? throw new ArgumentNullException(nameof(urlService));
            _backgroundSettingService = backgroundSettingService ?? throw new ArgumentNullException(nameof(urlService));
        }

        public async Task<IActionResult> Index()
        {           
            var applicationSettingModel = await _backgroundSettingService.GetBackgroundTimeAsync();
            var urlModels = await _urlService.GetAllAsync();
            var adminPageModel = new AdministrationPageModel(urlModels, applicationSettingModel);

            return View(adminPageModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UrlModel model)
        {
            if (ModelState.IsValid)
            {
                await _urlService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<ActionResult> Delete(string id)
        {
            await _urlService.RemoveAsync(id);
            return RedirectToAction(IndexActionName);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var model = await _urlService.TryGetById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UrlModel model)
        {
            if (ModelState.IsValid)
            {
                await _urlService.UpdateAsync(model.Id, model);
                return RedirectToAction(IndexActionName);
            }
            return View(model);
        }

        public async Task<ActionResult> EditLoopTime()
        {
            var model = await _backgroundSettingService.GetBackgroundTimeAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditLoopTime(BackgroundSettingModel model)
        {
            if (ModelState.IsValid)
            {
                await _backgroundSettingService.UpdateAsync(model.Id, model);
                return RedirectToAction(IndexActionName);
            }
            return View(model);
        }
    }
}
