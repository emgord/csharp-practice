using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodFinder
{
    public interface IPlaceService
    {
        Task<Place[]> FindPlaces(double lat, double lng);
    }
}
