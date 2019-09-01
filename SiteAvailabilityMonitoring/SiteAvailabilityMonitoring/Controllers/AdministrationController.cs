using Domain.Models;
using Infrastructure.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteAvailabilityMonitoring.Models;
using System.Threading.Tasks;

namespace SiteAvailabilityMonitoring.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        private readonly UrlCollectionService _urlCollectionService;
        private readonly BackgroundSettingsService _applicationSettingsService;

        public AdministrationController(UrlCollectionService urlCollectionService, BackgroundSettingsService applicationSettingsService)
        {
            _urlCollectionService = urlCollectionService;
            _applicationSettingsService = applicationSettingsService;
        }

        public async Task<IActionResult> Index()
        {           
            var applicationSettingModel = await _applicationSettingsService.GetBackgroundTimeAsync();
            var urlModels = await _urlCollectionService.GetAllAsync();
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
                await _urlCollectionService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<ActionResult> Delete(string id)
        {
            await _urlCollectionService.RemoveAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(string id)
        {
            var model = await _urlCollectionService.TryGetById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UrlModel model)
        {
            if (ModelState.IsValid)
            {
                await _urlCollectionService.UpdateAsync(model.Id, model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> EditLoopTime()
        {
            var model = await _applicationSettingsService.GetBackgroundTimeAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditLoopTime(BackgroundSettingModel model)
        {
            if (ModelState.IsValid)
            {
                await _applicationSettingsService.UpdateAsync(model.Id, model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
