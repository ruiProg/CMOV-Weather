using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class Region
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        public Region()
        {
            ID = 0;
            Name = "";
        }
    }
}
