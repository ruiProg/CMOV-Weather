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
        static String serverURL = "http://7111a0fa.ngrok.io";

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

        public static async Task<List<Country>> getCountries()
        {
            var queryString = String.Format("{0}/countries", serverURL);
            var response = await client.GetAsync(queryString);

            if (response != null)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Country>>(json);
            }

            return null;
        }

        public static async Task<List<Region>> getRegions(int countryID)
        {
            var queryString = String.Format("{0}/regions?country={1}", serverURL, countryID);
            var response = await client.GetAsync(queryString);

            if (response != null)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Region>>(json);
            }

            return null;
        }

        public static async Task<List<City>> getCities(int countryID, int regionID)
        {
            var queryString = String.Format("{0}/cities?country={1}&region={2}", serverURL, countryID, regionID);
            var response = await client.GetAsync(queryString);

            if (response != null)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<City>>(json);
            }

            return null;
        }
    }
}
