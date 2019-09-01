using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Infrastructure.Providers;
using Microsoft.AspNetCore.Mvc;
using SiteAvailabilityMonitoring.Models;

namespace SiteAvailabilityMonitoring.Controllers
{
    public class HomeController : Controller
    {
        private readonly UrlCollectionService _urlCollectionService;

        public HomeController(UrlCollectionService urlCollectionService)
        {
            _urlCollectionService = urlCollectionService ?? throw new ArgumentNullException(nameof(urlCollectionService));
        }

        public async Task<IActionResult> Index()
        {
            var models = await _urlCollectionService.GetAllAsync();
            return View(models);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
