using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FoodFinder.Controllers
{
    public class HomeController : Controller
    {
        private IMapService _mapService;

        public HomeController(IMapService mapService)
        {
            _mapService = mapService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(String start, String end)
        {
            var searchResult = await _mapService.FindRoute(start, end);
            return View("Search", searchResult);
        }
    }
}