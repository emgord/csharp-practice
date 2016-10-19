using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FoodFinder
{
    public class PlaceService : IPlaceService
    {
        public async Task<Place[]> FindPlaces(double lat, double lng)
        {
            string api_key = Environment.GetEnvironmentVariable("FOODFINDER_PLACES_API_KEY");
            string baseUrl = "https://maps.googleapis.com";
            string path = string.Format("/maps/api/place/nearbysearch/json?location={0},{1}&radius=1000&type=restaurant&key={2}", 
                lat, lng, api_key);

            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(path);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Places API call returned non-200");
            }

            var json = await response.Content.ReadAsStringAsync();
            var placeResponse = JsonConvert.DeserializeObject <PlaceResponse>(json);
            return placeResponse.results;
        }
    }

    class PlaceResponse
    {
        public Place[] results { get; set; }

    }
}
