using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.View.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = @"http://dataservice.accuweather.com/";
        public const string AUTO_COMPLETE_ENDPOINT = @"locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITION_ENDPOINT = @"currentconditions/v1/{0}?apikey={1}";
        public const string API_KEY = @"TVG2Dg2l3jJUygxjmIYjjHovryHHMyac";


        public async List<City> GetCities(string query)
        {
            var cities = new List<City>();
            var url = BASE_URL + string.Format(AUTO_COMPLETE_ENDPOINT, API_KEY, query);

            using (var client = new HttpClient()) 
            { 
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }
            return cities;
        }
    }

    
}
