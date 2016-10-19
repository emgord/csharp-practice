using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace FoodFinder
{
    public class MapService : IMapService
    {
        public async Task<SearchResult> FindRoute(string start, string end)
        {
            string api_key = Environment.GetEnvironmentVariable("FOODFINDER_MAPS_API_KEY");
            string baseUrl = "https://maps.googleapis.com";
            string path = string.Format("/maps/api/directions/json?origin={0}&destination={1}&key={2}", start, end,
                api_key);


            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var response = await client.GetAsync(path);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Directions API call returned non-200");
            }

            var json = await response.Content.ReadAsStringAsync();
            var routeResponse = JsonConvert.DeserializeObject<RouteResponse>(json);
            var leg = routeResponse.routes[0].legs[0];
            return new SearchResult(leg.start_location.lat, leg.start_location.lng, leg.end_location.lat,
                leg.end_location.lng);
        }

        private class RouteResponse
        {
            public String status { get; set; }

            public Route[] routes { get; set; }
        }

        private class Route
        {
            public Leg[] legs { get; set; }

        }

        private class Leg
        {
            public Location start_location { get; set; }
            public Location end_location { get; set; }
        }

        private class Location
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }
    }
}
