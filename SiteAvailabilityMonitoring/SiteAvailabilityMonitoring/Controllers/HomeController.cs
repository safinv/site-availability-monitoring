using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using SiteAvailabilityMonitoring.Domain.Database.Contracts;
using SiteAvailabilityMonitoring.Domain.Models;
using SiteAvailabilityMonitoring.Models;

namespace SiteAvailabilityMonitoring.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbQuery<Site> _siteQuery;

        public HomeController(IDbQuery<Site> siteQuery)
        {
            _siteQuery = siteQuery ?? throw new ArgumentNullException(nameof(siteQuery));
        }

        public async Task<IActionResult> Index()
        {
            var models = await _siteQuery.GetAllAsync();
            return View(models);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}