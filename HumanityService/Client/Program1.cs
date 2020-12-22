using HumanityService.DataContracts.Requests;
using HumanityService.DataContracts.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program1
    {
        private static readonly HttpClient client = new HttpClient();
        private static Uri baseAddress = new Uri("https://api.openrouteservice.org/v2/matrix/");

        static async Task Main(string[] args)
        {
            var coord1 = new List<double>();
            coord1.Add(35.600181);
            coord1.Add(33.905057899999996);

            var coord2 = new List<double>();
            coord2.Add(35.20980834960938);
            coord2.Add(33.26624989076275);

            var coord = new List<List<double>>();
            coord.Add(coord1);
            coord.Add(coord2);

            var x = new List<(double, string)>()
            {
                (0.31, "xSOUI"),
                (87, "aIJDB"),
                (76, "zIUGIO")
            };

            x.Sort();
 

            //var baseAddress = new Uri("https://api.openrouteservice.org");

            //using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            //{
            //    httpClient.DefaultRequestHeaders.Clear();
            //    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");
            //    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "5b3ce3597851110001cf6248c8f842cc78114de5ac5491e15d186abf");

            //    var loc = new OpenrouteserviceMatrixRequest
            //    {
            //        locations = coord
            //    };
            //    var json = JsonConvert.SerializeObject(loc);
            //    // quotes might have to be escaped
            //    using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
            //    {
            //        using (var response = await httpClient.PostAsync("/v2/matrix/driving-car", content))
            //        {
            //            string responseData = await response.Content.ReadAsStringAsync();
            //            var data = JsonConvert.DeserializeObject<OpenrouteserviceMatrixResponse>(responseData);
            //        }
            //    }
            //}
        }


        private static string BuildUrl(string transportation)
        {
            //validate transportation type before
            return baseAddress + transportation;
        }

    }
}
