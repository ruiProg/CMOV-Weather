// Helpers/Settings.cs
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Collections.Generic;
using WeatherApp.Models;

namespace WeatherApp.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

        #region Setting Constants

        private const string myCities = "my_cities";
        private const string selectedCity = "current_city";

        #endregion

        public static bool GetUnit(string key)
        {
            return AppSettings.GetValueOrDefault(key, false);
        }

        public static bool SetUnit(string key, bool value)
        {
            return AppSettings.AddOrUpdateValue(key, value);
        }


        public static List<CityDetails> MyCitiesList
        {
            get
            {
                string value = AppSettings.GetValueOrDefault(myCities, string.Empty);
                List<CityDetails> myList;
                if (string.IsNullOrEmpty(value))
                    myList = new List<CityDetails>();
                else
                    myList = JsonConvert.DeserializeObject<List<CityDetails>>(value);
                return myList;
            }
            set
            {
                string listValue = JsonConvert.SerializeObject(value);
                AppSettings.AddOrUpdateValue(myCities, listValue);
            }
        }

        public static CityDetails CurrentCity
        {
            get
            {
                string value = AppSettings.GetValueOrDefault(selectedCity, string.Empty);
                if (string.IsNullOrEmpty(value))
                    return new CityDetails();
                else return JsonConvert.DeserializeObject<CityDetails>(value);
            }
            set
            {
                string valueStr = JsonConvert.SerializeObject(value);
                AppSettings.AddOrUpdateValue(selectedCity, valueStr);
            }
        }
    }
}