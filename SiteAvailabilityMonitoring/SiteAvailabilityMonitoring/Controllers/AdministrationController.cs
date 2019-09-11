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
        private readonly IDbCommand<Site> _siteCommand;
        private readonly IDbCommand<Background> _backgroundCommand;

        private const string IndexActionName = "Index";

        public AdministrationController(IDbCommand<Site> siteCommand, IDbCommand<Background> backgroundCommand)
        {
            _siteCommand = siteCommand ?? throw new ArgumentNullException(nameof(siteCommand));
            _backgroundCommand = backgroundCommand ?? throw new ArgumentNullException(nameof(backgroundCommand));
        }

        public async Task<IActionResult> Index()
        {
            var applicationSettingModel = await _backgroundCommand.Query.GetAsync(b => b.Type == "Background");
            var sites = await _siteCommand.Query.GetAllAsync();
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
                _siteCommand.Add(model);
                await _siteCommand.Commit();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<ActionResult> Delete(string id)
        {
            _siteCommand.Remove(id);
            await _siteCommand.Commit();

            return RedirectToAction(IndexActionName);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var model = await _siteCommand.Query.GetAsync(s => s.Id == id);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Site model)
        {
            if (ModelState.IsValid)
            {
                _siteCommand.Update(model);
                await _siteCommand.Commit();

                return RedirectToAction(IndexActionName);
            }
            return View(model);
        }

        public async Task<ActionResult> EditLoopTime()
        {
            var model = await _backgroundCommand.Query.GetAsync(b => b.Type == "Background");
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditLoopTime(Background model)
        {
            if (ModelState.IsValid)
            {
                _backgroundCommand.Update(model);
                await _backgroundCommand.Commit();

                return RedirectToAction(IndexActionName);
            }
            return View(model);
        }
    }
}