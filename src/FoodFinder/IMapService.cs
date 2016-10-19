using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodFinder
{
    public interface IMapService
    {
        Task<SearchResult> FindRoute(string start, string end);
    }
}
