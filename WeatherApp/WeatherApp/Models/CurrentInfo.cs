using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    class CurrentInfo
    {
        public String City { get; set; }

        public String RegionName { get; set; }

        public String LastUpdate { get; set; }

        public String Temperature { get; set; }

        public String CondText { get; set; }

        public String CondImage { get; set; }

        public CurrentInfo()
        {
            City = "";
            RegionName = "";
            LastUpdate = "";
            Temperature = "";
            CondText = "";
            CondImage = "";
        }

        public CurrentInfo(CurrentTemp temp)
        {
            City = temp.Location.City;
            RegionName = temp.Location.GeoRegion + ", " + temp.Location.Country;
            LastUpdate = String.Format("Last update: {0:r}", temp.Info.LastUpdate);
            CondText = temp.Info.Description.Text;
            CondImage = (temp.Info.DayFlag ? "d" : "n") + Path.GetFileName(temp.Info.Description.IconPath);

            if (Helpers.Settings.GetUnit(Unit.tempUnit))
            {
                Temperature = temp.Info.TempFahr + " °F";
            }
            else
            {
                Temperature = temp.Info.TempCelsisus + " °C";
            }

        }
    }
}
