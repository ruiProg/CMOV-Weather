using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WeatherApp.Models;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class CurrentConditions : CarouselPage
    {
        private ObservableCollection<CurrentInfo> All { get; set; }

        int lastUpdated = -1;

        public CurrentConditions()
        {
            var listCities = Helpers.Settings.MyCitiesList;
            InitializeComponent();
            ItemsSource = null;

            if (listCities.Any())
            {
                All = new ObservableCollection<CurrentInfo>();
                foreach (var city in Helpers.Settings.MyCitiesList)
                    All.Add(new CurrentInfo());
                RetrieveWeather(0);
            }
            if (Device.RuntimePlatform == Device.Android)
                BackgroundColor = Color.FromHex("#0f4727");
        }

        async void RetrieveWeather(int index)
        {
            if (index == -1)
                index = 0;
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
            lastUpdated++;
            All[index] = data;
            ItemsSource = All;
        }

        private void OnPageChanged(object sender, EventArgs e)
        {
            var index = Children.IndexOf(this.CurrentPage);
            if (ItemsSource == null || index > lastUpdated)
                RetrieveWeather(index);
        }
    }
}

