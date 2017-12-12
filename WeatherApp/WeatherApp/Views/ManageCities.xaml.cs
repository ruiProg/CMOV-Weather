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
    public partial class ManageCities : ContentPage
    {
        private ObservableCollection<CityDetails> Cities { get; set; }

        private List<Country> Countries { get; set; } = null;

        private List<Region> Regions { get; set; }

        private List<City> RegionCities { get; set; }


        public ManageCities()
        {
            RetrieveCountries();
            Cities = new ObservableCollection<CityDetails>();
            InitializeComponent();
            citiesView.ItemsSource = Cities;

            foreach (var city in Helpers.Settings.MyCitiesList)
                Cities.Add(city);

            /*Helpers.Settings.MyCitiesList = new List<CityDetails>()
            {
                new CityDetails() {ID = 1, Name="Porto", Details="Porto, Portugal"},
                new CityDetails() {ID = 1, Name="Lamego", Details="Viseu, Portugal"}
            };*/
        }

        void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            var item = (CityDetails)e.SelectedItem;
            Helpers.Settings.CurrentCity = item;
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            var city = (CityDetails) item.CommandParameter;
            Cities.Remove(city);
            Helpers.Settings.MyCitiesList = Cities.ToList();
        }

        async void RetrieveCountries()
        {
            if (Countries == null)
            {
                Countries = await Service.getCountries();
                countriesList.ItemsSource = Countries;
            }
        }

        async void OnCountrySelected(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                int countryID = ((Country)picker.ItemsSource[selectedIndex]).ID;
                Regions = await Service.getRegions(countryID);
                regionsList.ItemsSource = Regions;
                citiesList.ItemsSource = null;
            }
        }

        async void OnRegionSelected(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                int countryID = ((Country)countriesList.ItemsSource[countriesList.SelectedIndex]).ID;
                int regionID = ((Region)picker.ItemsSource[selectedIndex]).ID;
                RegionCities = await Service.getCities(countryID, regionID);
                citiesList.ItemsSource = RegionCities;
            }
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            int selectedIndex = citiesList.SelectedIndex;
            if (selectedIndex != -1)
            {
                var newCity = ((City)citiesList.ItemsSource[citiesList.SelectedIndex]);
                Cities.Add(new CityDetails(newCity));
                Helpers.Settings.MyCitiesList = Cities.ToList();
            }
            else await DisplayAlert("Alert", "No city selected", "OK");
        }
    }
}