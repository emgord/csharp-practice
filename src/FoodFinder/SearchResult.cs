using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodFinder
{
    public struct Coordinate
    {
        public double Latitude;
        public double Longitude;
    }

    public class SearchResult
    {
        public SearchResult(double startLat, double startLong, double endLat, double endLong)
        {
            Start = new Coordinate()
            {
                Latitude = startLat,
                Longitude = startLong
            };
            End = new Coordinate()
            {
                Latitude = endLat,
                Longitude = endLong
            };
        }

        public Coordinate Start { get; }
        public Coordinate End  { get; }
    }
}
