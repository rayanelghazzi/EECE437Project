using HumanityService.DataContracts.CompositeDesignPattern;
using HumanityService.DataContracts.Requests;
using HumanityService.DataContracts.Results;
using HumanityService.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HumanityService.Services
{
    public class RoutingService : IRoutingService
    {
        private Uri baseAddress = new Uri("https://api.openrouteservice.org/v2/matrix/");

        public async Task<double> GetETA(Location delivererCoordinates, Location donorCoordinates, Location ngoCoordinates, string transportationType)
        {
            var coord1 = new List<double>
            {
                delivererCoordinates.Longitude,
                delivererCoordinates.Latitude
            };

            var coord2 = new List<double>()
            {
                donorCoordinates.Longitude,
                donorCoordinates.Latitude
            };

            var coord3 = new List<double>
            {
                ngoCoordinates.Longitude,
                ngoCoordinates.Latitude
            };

            var coord = new List<List<double>>
            {
                coord1,
                coord2,
                coord3
            };

            var matrix = await GetTimeDistanceMatrix(coord, transportationType);
            var durationMatrix = matrix.durations;
            var eta = durationMatrix[0][1] + durationMatrix[1][2]; //deliverer-donor + donor-ngo duration time by given transportation type


            return eta;
        }

        public async Task<double> GetETA(Location volunteerCoordinates, Location ngoCoordinates, string transportationType)
        {
            var coord1 = new List<double>()
            {
                volunteerCoordinates.Longitude,
                volunteerCoordinates.Latitude
            };

            var coord2 = new List<double>
            {
                ngoCoordinates.Longitude,
                ngoCoordinates.Latitude
            };

            var coord = new List<List<double>>
            {
                coord1,
                coord2
            };

            var matrix = await GetTimeDistanceMatrix(coord, transportationType);
            var durationMatrix = matrix.durations;
            var eta = durationMatrix[0][1]; //volunteer-ngo duration time by given transportation type

            return eta;
        }


        private async Task<OpenrouteserviceMatrixResponse> GetTimeDistanceMatrix(List<List<double>> coordinatesMatrix, string transportationType)
        {
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "5b3ce3597851110001cf6248c8f842cc78114de5ac5491e15d186abf");

                var loc = new OpenrouteserviceMatrixRequest
                {
                    locations = coordinatesMatrix
                };
                var json = JsonConvert.SerializeObject(loc);
                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    var url = BuildUrl(transportationType);
                    using (var response = await httpClient.PostAsync(url, content))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<OpenrouteserviceMatrixResponse>(responseData);
                        return data;
                    }
                }
            }
        }


        private string BuildUrl(string transportation)
        {
            //validate transportation type before
            return "/v2/matrix/" + transportation;
        }
    }
}
