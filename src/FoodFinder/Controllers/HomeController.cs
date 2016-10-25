using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodFinder.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
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

        public async Task<IActionResult> Search(String start, String end, String stopover = null)
        {
            var searchResult = await _mapService.FindRoute(start, end);
            var startPlacesResult = await _placeService.FindPlaces(searchResult.Start.Latitude, searchResult.Start.Longitude);
            var endPlacesResult = await _placeService.FindPlaces(searchResult.End.Latitude, searchResult.End.Longitude);
            var allPlacesResults = startPlacesResult.Concat(endPlacesResult).ToArray();
            var mapUrl = string.Format("https://www.google.com/maps/embed/v1/directions?key={0}&origin={1}&destination={2}",
                Environment.GetEnvironmentVariable("FOODFINDER_MAPS_API_KEY"), start.Replace(" ", "+"), end.Replace(" ", "+"));
            if (stopover != null)
            {
                mapUrl += string.Format("&waypoints=place_id:{0}", stopover);
            }

            var currentUrl = HttpContext.Request.GetDisplayUrl();
            currentUrl = currentUrl.Split(new string[] {"&stopover"}, StringSplitOptions.None)[0];

            var searchViewModel = new SearchViewModel(currentUrl, allPlacesResults, mapUrl);

            return View("Search", searchViewModel);
        }
    }
}