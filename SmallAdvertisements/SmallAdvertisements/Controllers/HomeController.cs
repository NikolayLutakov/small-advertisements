using Microsoft.AspNetCore.Mvc;
using SmallAdvertisements.Models;
using SmallAdvertisements.Models.ViewModels.Home;
using SmallAdvertisements.Services.Contracts;
using System.Diagnostics;

namespace SmallAdvertisements.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdvertisementService _advertisementService;

        public HomeController(ILogger<HomeController> logger,IAdvertisementService advertisementService)
        {
            _logger = logger;
            _advertisementService= advertisementService;
        }

        public IActionResult Index()
        {
            var advertisements = _advertisementService.GetAll();

            var indexViewModel = new IndexViewModel()
            {
                Advertisements = advertisements
            };

            return View(indexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}