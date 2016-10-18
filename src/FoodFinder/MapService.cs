using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodFinder
{
    public class MapService : IMapService
    {
        public SearchResult FindRoute(string start, string end)
        {
            return new SearchResult(123.4, 12.3, 13.4, 124.5);
        }
    }
}
