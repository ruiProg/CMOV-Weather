using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentConditions : ContentPage
    {
        CurrentInfo data;

        public CurrentConditions()
        {
            var currCity = Helpers.Settings.CurrentCity;
            App.currentCity = currCity.Name + ", " + currCity.Details;
            RetrieveWeather();
            InitializeComponent();
            if (Device.RuntimePlatform == Device.Android)
                BackgroundColor = Color.FromHex("#0f4727");
        }

        async void RetrieveWeather()
        {
            CurrentTemp temp = null;
            try
            {
                temp = await Service.getCurrentWeather();
            }
            catch (Exception)
            {
            }
            if (temp == null)
            {
                temp = new CurrentTemp();
                await DisplayAlert("Alert", "Service is not available", "OK");
            }
            data = new CurrentInfo(temp);
            this.BindingContext = data;
        }
    }
}