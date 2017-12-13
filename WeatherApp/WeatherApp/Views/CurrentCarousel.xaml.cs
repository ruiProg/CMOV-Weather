using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CurrentCarousel : CarouselPage
	{
        private ObservableCollection<CityDetails> CurrentCities { get; set; }

        public CurrentCarousel ()
		{
            var listCities = Helpers.Settings.MyCitiesList;
            
            if (listCities.Any())
            {
                CurrentCities = new ObservableCollection<CityDetails>();
                InitializeComponent();
                ItemsSource = CurrentCities;
                foreach (var city in Helpers.Settings.MyCitiesList)
                   CurrentCities.Add(new CityDetails());
                RetrieveWeather();
                System.Diagnostics.Debug.WriteLine("Heree");
                if (Device.RuntimePlatform == Device.Android)
                    BackgroundColor = Color.FromHex("#0f4727");
            }
            else ShowError();
        }

        async public void ShowError()
        {
            await DisplayAlert("Alert", "No cities available yet", "OK");
        }

        async void RetrieveWeather()
        {
            var index = Children.IndexOf(this.CurrentPage);
            var currentCity = Helpers.Settings.MyCitiesList[index];
            App.currentCity = currentCity.Name + ", " + currentCity.Details;
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
            var data = new CurrentInfo(temp);
            CurrentCities[index] = new CityDetails();
            ItemsSource = CurrentCities;
        }

        private void OnPageChanged(object sender, EventArgs e)
        {
            base.OnCurrentPageChanged();
            RetrieveWeather();
        }
    }
}