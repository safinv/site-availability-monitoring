using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SiteAvailabilityMonitoring.Domain.Database.Contracts;
using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Models;

namespace SiteAvailabilityMonitoring.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        private readonly IDbRequest<Site> _siteRequest;
        private readonly IDbRequest<Background> _backgroundRequest;

        private readonly IDbQuery<Site> _siteQuery;
        private readonly IDbQuery<Background> _backgroundQuery;

        private const string IndexActionName = "Index";

        public AdministrationController(
            IDbRequest<Site> siteRequest,
            IDbRequest<Background> backgroundRequest,
            IDbQuery<Site> siteQuery,
            IDbQuery<Background> backgroundQuery)
        {
            _siteRequest = siteRequest ?? throw new ArgumentNullException(nameof(siteRequest));
            _backgroundRequest = backgroundRequest ?? throw new ArgumentNullException(nameof(backgroundRequest));

            _siteQuery = siteQuery ?? throw new ArgumentNullException(nameof(siteQuery));
            _backgroundQuery = backgroundQuery ?? throw new ArgumentNullException(nameof(backgroundQuery));
        }

        public async Task<IActionResult> Index()
        {
            var applicationSettingModel = await _backgroundQuery.GetAsync(b => b.Type == "Background");
            var sites = await _siteQuery.GetAllAsync();
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
                await _siteRequest.AddAsync(model);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<ActionResult> Delete(string id)
        {
            await _siteRequest.RemoveAsync(id);

            return RedirectToAction(IndexActionName);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var model = await _siteQuery.GetAsync(s => s.Id == id);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Site model)
        {
            if (ModelState.IsValid)
            {
                await _siteRequest.UpdateAsync(model);

                return RedirectToAction(IndexActionName);
            }
            return View(model);
        }

        public async Task<ActionResult> EditLoopTime()
        {
            var model = await _backgroundQuery.GetAsync(b => b.Type == "Background");
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditLoopTime(Background model)
        {
            if (ModelState.IsValid)
            {
                await _backgroundRequest.UpdateAsync(model);

                return RedirectToAction(IndexActionName);
            }
            return View(model);
        }
    }
}