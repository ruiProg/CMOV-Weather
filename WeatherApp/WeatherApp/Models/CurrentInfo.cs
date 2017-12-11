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

        public String WindImage { get; set; }

        public String WindSpeed { get; set; }

        public String WindDir { get; set; }

        public String HumidityImage { get; set; }
        
        public String Humidity { get; set; }

        public String FeelsLike { get; set; }

        public String PrecipitationImage { get; set; }

        public String Precipitation { get; set; }

        public String VisibilityImage { get; set; }

        public String Clouds { get; set; }

        public String Visibility { get; set; }

        public CurrentInfo()
        {
            City = "";
            RegionName = "";
            LastUpdate = "";
            Temperature = "";
            CondText = "";
            CondImage = "";
            WindSpeed = "";
            WindDir = "";
            WindImage = "wind.png";
            HumidityImage = "humidity.png";
            PrecipitationImage = "precipitation.png";
            Humidity = "";
            FeelsLike = "";
            Precipitation = "";
            VisibilityImage = "visibility.png";
            Clouds = "";
            Visibility = "";
        }

        public CurrentInfo(CurrentTemp temp) : this()
        {
            City = temp.Location.City;
            RegionName = temp.Location.GeoRegion + ", " + temp.Location.Country;
            LastUpdate = String.Format("Last update: {0:r}", temp.Info.LastUpdate);
            CondText = temp.Info.Description.Text;
            CondImage = (temp.Info.DayFlag ? "d" : "n") + Path.GetFileName(temp.Info.Description.IconPath);

            if (Helpers.Settings.GetUnit(Unit.tempUnit))
            {
                Temperature = temp.Info.TempFahr + " °F";
                FeelsLike = "Feels like: " +  temp.Info.FeelsFahr + " °F";
            }
            else
            {
                Temperature = temp.Info.TempCelsisus + " °C";
                FeelsLike = "Feels like: " + temp.Info.FeelsCelsius + " °C";
            }
            if (Helpers.Settings.GetUnit(Unit.windUnit))
                WindSpeed = temp.Info.WindMPH + " mph";
            else WindSpeed = temp.Info.WindKPH + " km/h";
            WindDir = String.Format("{0}° {1}", temp.Info.WindDegrees, temp.Info.WindDir);
            Humidity = "Humidity: " + temp.Info.Humidity + "%";
            if (Helpers.Settings.GetUnit(Unit.precUnit))
                Precipitation = temp.Info.PrecInches + " in";
            else Precipitation = temp.Info.PrecMeters + " mm";
            Clouds = "Cloud cover: " + temp.Info.Cloud + " %";
            if (Helpers.Settings.GetUnit(Unit.visUnit))
                Visibility = "Visibility: " + temp.Info.VisMiles + " mi";
            else Visibility = "Visibility: " + temp.Info.VisKms + " km";
        }
    }
}
