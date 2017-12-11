using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    class CurrentTemp
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("current")]
        public InfoData Info { get; set; }

        public CurrentTemp()
        {
            Location = new Location();
            Info = new InfoData();
        }
    }

    class Location
    {
        [JsonProperty("name")]
        public String City { get; set; }

        [JsonProperty("region")]
        public String GeoRegion { get; set; }

        [JsonProperty("country")]
        public String Country { get; set; }

        public Location()
        {
            City = "";
            GeoRegion = "";
            Country = "";
        }
    }

    class InfoData
    {
        [JsonProperty("last_updated")]
        public DateTime LastUpdate { get; set; }

        [JsonProperty("temp_c")]
        public float TempCelsisus { get; set; }

        [JsonProperty("temp_f")]
        public float TempFahr { get; set; }

        [JsonProperty("is_day")]
        public bool DayFlag { get; set; }

        [JsonProperty("condition")]
        public Condition Description { get; set; }

        [JsonProperty("wind_mph")]
        public float WindMPH { get; set; }

        [JsonProperty("wind_degree")]
        public int WindDegrees { get; set; }

        [JsonProperty("wind_kph")]
        public float WindKPH { get; set; }

        [JsonProperty("wind_dir")]
        public String WindDir { get; set; }

        [JsonProperty("precip_mm")]
        public float PrecMeters { get; set; }

        [JsonProperty("precip_in")]
        public float PrecInches { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("cloud")]
        public int Cloud { get; set; }

        [JsonProperty("feelslike_c")]
        public float FeelsCelsius { get; set; }

        [JsonProperty("feelslike_f")]
        public float FeelsFahr { get; set; }

        [JsonProperty("vis_km")]
        public float VisKms { get; set; }

        [JsonProperty("vis_miles")]
        public float VisMiles { get; set; }

        public InfoData()
        {
            LastUpdate = new DateTime();
            TempCelsisus = 0f;
            TempFahr = 0f;
            DayFlag = true;
            Description = new Condition();
            WindMPH = 0f;
            WindKPH = 0f;
            WindDegrees = 0;
            WindDir = "";
            PrecInches = 0f;
            PrecMeters = 0f;
            Humidity = 0;
            Cloud = 0;
            FeelsCelsius = 0f;
            FeelsFahr = 0f;
            VisKms = 0f;
            VisMiles = 0f;
        }
    }

    class Condition
    {
        [JsonProperty("text")]
        public String Text { get; set; }

        [JsonProperty("icon")]
        public String IconPath { get; set; }

        public Condition()
        {
            Text = "";
            IconPath = "";
        }
    }
}
