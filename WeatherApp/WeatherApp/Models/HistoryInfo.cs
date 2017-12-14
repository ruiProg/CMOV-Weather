using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{

    
    public class HistoryInfo
    {

        public String City { get; set; }

        public String RegionName { get; set; }

        public String MaxTemperature { get; set; }

        public String MinTemperature { get; set; }

        public String AvgTemperature { get; set; }

        public String CondText { get; set; }

        public String CondImage { get; set; }

        public String WindImage { get; set; }

        public String MaxWindSpeed { get; set; }

        public String HumidityImage { get; set; }

        public String AvgHumidity { get; set; }

        public String PrecipitationImage { get; set; }

        public String TotalPrecipitation { get; set; }

        public String VisibilityImage { get; set; }

        public String AvgVisibility { get; set; }

        public List<HistoryData> Hours { get; set; }

        public HistoryInfo()
        {
            City = "";
            RegionName = "";
            MaxTemperature = "";
            MinTemperature = "";
            AvgTemperature = "";
            CondText = "";
            CondImage = "";
            MaxWindSpeed = "";
            WindImage = "wind.png";
            HumidityImage = "humidity.png";
            PrecipitationImage = "precipitation.png";
            AvgHumidity = "";

            TotalPrecipitation = "";
            VisibilityImage = "visibility.png";
            AvgVisibility = "";

            Hours = new List<HistoryData>();
        }

        public HistoryInfo(HistoryTemp temp) : this()
        {
            City = temp.Location.City;
            RegionName = temp.Location.GeoRegion + ", " + temp.Location.Country;
            var tempData = temp.Forecast.Forecastday[0].Forecastdata;
            CondText = tempData.Day.Description.Text;
            CondImage = "d" + Path.GetFileName(tempData.Day.Description.IconPath);

            if (Helpers.Settings.GetUnit(Unit.tempUnit))
            {
                MaxTemperature = tempData.Day.MaxTempFahr + " °F";
                MinTemperature = tempData.Day.MinTempFahr + " °F";
                AvgTemperature = tempData.Day.AvgTempFahr + " °F";
            }
            else
            {
                MaxTemperature = tempData.Day.MaxTempCelsisus + " °C";
                MinTemperature = tempData.Day.MinTempCelsisus + " °C";
                AvgTemperature = tempData.Day.AvgTempCelsisus + " °C";
            }
            if (Helpers.Settings.GetUnit(Unit.windUnit))
                MaxWindSpeed = tempData.Day.MaxWindMPH + " mph";
            else MaxWindSpeed = tempData.Day.MaxWindKPH + " km/h";
            AvgHumidity = "Avg Humidity: " + tempData.Day.AvgHumidity + "%";
            if (Helpers.Settings.GetUnit(Unit.precUnit))
                TotalPrecipitation = tempData.Day.TotalPrecInches + " in";
            else TotalPrecipitation = tempData.Day.TotalPrecMeters + " mm";
            if (Helpers.Settings.GetUnit(Unit.visUnit))
                AvgVisibility = "Visibility: " + tempData.Day.AvgVisMiles + " mi";
            else AvgVisibility = "Visibility: " + tempData.Day.AvgVisKms + " km";

            Hours = tempData.Hours;

        }

    }
}
