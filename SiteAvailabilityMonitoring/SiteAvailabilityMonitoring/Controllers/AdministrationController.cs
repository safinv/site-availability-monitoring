using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Infrastructure.Repositories;
using SiteAvailabilityMonitoring.Models;

namespace SiteAvailabilityMonitoring.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        private readonly SiteRepository _siteRepository;
        private readonly BackgroundRepository _backgroundRepository;

        private const string IndexActionName = "Index";

        public AdministrationController(SiteRepository siteRepository, BackgroundRepository backgroundRepository)
        {
            _siteRepository = siteRepository ?? throw new ArgumentNullException(nameof(siteRepository));
            _backgroundRepository = backgroundRepository ?? throw new ArgumentNullException(nameof(siteRepository));
        }

        public async Task<IActionResult> Index()
        {
            var applicationSettingModel = await _backgroundRepository.GetAsync(b => b.Type == "Background");
            var sites = await _siteRepository.GetAllAsync();
            var adminPageModel = new AdministrationPageModel(sites, applicationSettingModel);

            return View(adminPageModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Site model)
        {
            if (ModelState.IsValid)
            {
                await _siteRepository.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<ActionResult> Delete(string id)
        {
            await _siteRepository.RemoveAsync(id);
            return RedirectToAction(IndexActionName);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var model = await _siteRepository.GetAsync(s => s.Id == id);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Site model)
        {
            if (ModelState.IsValid)
            {
                await _siteRepository.UpdateAsync(model);
                return RedirectToAction(IndexActionName);
            }
            return View(model);
        }

        public async Task<ActionResult> EditLoopTime()
        {
            var model = await _backgroundRepository.GetAsync(b => b.Type == "Background");
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditLoopTime(Background model)
        {
            if (ModelState.IsValid)
            {
                await _backgroundRepository.UpdateAsync(model);
                return RedirectToAction(IndexActionName);
            }
            return View(model);
        }
    }
}