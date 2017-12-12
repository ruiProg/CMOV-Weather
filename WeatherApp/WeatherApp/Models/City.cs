using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class City
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("region")]
        public String Region { get; set; }

        [JsonProperty("country")]
        public String Country { get; set; }

        public City()
        {
            ID = 0;
            Name = "";
            Region = "";
            Country = "";
            
        }
    }
}
