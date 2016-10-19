using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MapsApiTester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TryCall().Wait();
            Console.Out.WriteLine("Hello, World");
        }

        private static async Task TryCall()
        {
            string baseUrl = "https://maps.googleapis.com";
            string path = "/maps/api/directions/json?origin=Disneyland&destination=Universal+Studios+Hollywood4&key=" +
                Environment.GetEnvironmentVariable("FOODFINDER_MAPS_API_KEY");
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var routeResponse = JsonConvert.DeserializeObject<RouteResponse>(json);
                Console.Out.WriteLine("Start Lat: " + routeResponse.routes[0].legs[0].start_location.lat);
            }

        }
    }

    class RouteResponse
    {
        public String status { get; set; }

        public Route[] routes { get; set; }
    }

    class Route
    {
        public Leg[] legs { get; set; }
        
    }

    class Leg
    {
        public Location start_location { get; set; }
        public Location end_location { get; set; }
    }

    class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}
