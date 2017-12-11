// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

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

		//private const string TempKey = "temp_unit";

        #endregion

        public static bool GetUnit(string key)
        {
            return AppSettings.GetValueOrDefault(key, false);
        }

        public static bool SetUnit(string key, bool value)
        {
            return AppSettings.AddOrUpdateValue(key, value);
        }
    }
}