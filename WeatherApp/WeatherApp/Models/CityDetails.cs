using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class CityDetails
    {
        public int ID { get; set; }

        public String Name { get; set; }

        public String Details { get; set; }

        public CityDetails()
        {
            ID = 0;
            Name = "";
            Details = "";

        }

        public CityDetails(City city)
        {
            ID = city.ID;
            Name = city.Name;
            Details = city.Region + ", " + city.Country;

        }
    }
}
