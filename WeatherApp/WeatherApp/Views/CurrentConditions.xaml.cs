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
        private double width = 0;
        private double height = 0;
        public static string orientation = "";

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
                for (int i = 0; i < Helpers.Settings.MyCitiesList.Count; i++)
                {
                    RetrieveWeather(i);
                }

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
            All[index] = data;
            lastUpdated++;
            ItemsSource = All;
        }

        private void OnPageChanged(object sender, EventArgs e)
        {
            var index = Children.IndexOf(CurrentPage);
            if (ItemsSource == null || index > lastUpdated)
                RetrieveWeather(index);
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {

            var index = Children.IndexOf(CurrentPage);
            
            var currentCity = Helpers.Settings.MyCitiesList[index];
            string currentCityDetails = currentCity.Name + ", " + currentCity.Details;
            Button button = (Button)sender;
            var modalPage = new DatepickerModal(currentCityDetails);
            await Navigation.PushAsync(modalPage);
            /*var historyPage = new History(currentCityDetails);
            await Navigation.PushAsync(historyPage);*/
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {

                this.width = width;
                this.height = height;


                if (width > height)
                {
                    //outerStack.Orientation = StackOrientation.Horizontal;
                }
                else
                {
                    // outerStack.Orientation = StackOrientation.Vertical;
                }

            }
        }
    }
}

