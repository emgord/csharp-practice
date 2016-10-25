using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FoodFinder.ViewModels
{
    public class SearchViewModel
    {
        private string _currentUrl;

        public SearchViewModel(string currentUrl, Place[] placeResults, string mapUrl)
        {
            _currentUrl = currentUrl;
            MapUrl = mapUrl;
            Places = placeResults;

        }

        public Place[] Places { get;}
        public string MapUrl { get;}

        public String buildStopoverLink(String stopover)
        {
            return String.Format("{0}&stopover={1}", _currentUrl, stopover);
        }
    }
}
