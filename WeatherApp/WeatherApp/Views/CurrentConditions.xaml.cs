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
            App.currentCity = "Porto";
            RetrieveWeather();
            InitializeComponent();
        }

        async void RetrieveWeather()
        {
            var temp = await Service.getCurrentWeather();
            System.Diagnostics.Debug.WriteLine(temp.Info.LastUpdate);
            data = new CurrentInfo(temp);
            this.BindingContext = data;
        }
    }
}