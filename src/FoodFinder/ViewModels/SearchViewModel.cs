using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodFinder.ViewModels
{
    public class SearchViewModel
    {
        public SearchViewModel(Place[] placeResults, string mapUrl)
        {
            MapUrl = mapUrl;
            Places = placeResults;

        }

        public Place[] Places { get;}
        public string MapUrl { get;}
    }
}
