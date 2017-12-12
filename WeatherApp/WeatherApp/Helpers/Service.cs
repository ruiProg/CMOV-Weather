using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp
{
    class Service
    {
        static HttpClient client = new HttpClient();
        static String apixuKey = "48de77b9d0584523a65161204170812";
        static String apixuBaseUrl = "https://api.apixu.com/v1";

        public static async Task<CurrentTemp> getCurrentWeather()
        {
            if (string.IsNullOrEmpty(App.currentCity))
                throw new InvalidOperationException("This cannot be called without a city");
            var queryString = String.Format("{0}/current.json?key={1}&q={2}", apixuBaseUrl, apixuKey, App.currentCity);
            var response = await client.GetAsync(queryString);

            if (response != null) {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CurrentTemp>(json);
            }

            return null;
        }
    }
}
