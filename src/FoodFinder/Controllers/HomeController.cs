using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FoodFinder.Controllers
{
    public class HomeController : Controller
    {
        private IMapService _mapService;
        private IPlaceService _placeService;

        public HomeController(IMapService mapService, IPlaceService placeService)
        {
            _mapService = mapService;
            _placeService = placeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(String start, String end)
        {
            var searchResult = await _mapService.FindRoute(start, end);
            var startPlacesResult = await _placeService.FindPlaces(searchResult.Start.Latitude, searchResult.Start.Longitude);
            var endPlacesResult = await _placeService.FindPlaces(searchResult.End.Latitude, searchResult.End.Longitude);
            var allPlacesResults = startPlacesResult.Concat(endPlacesResult).ToArray();

            return View("Search", allPlacesResults);
        }
    }
}