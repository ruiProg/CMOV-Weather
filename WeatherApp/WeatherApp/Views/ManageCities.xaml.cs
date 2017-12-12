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

        private List<Country> Countries { get; set; }

        private List<Region> Regions { get; set; }

        private List<City> RegionCities { get; set; }


        public ManageCities()
        {
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

    }
}