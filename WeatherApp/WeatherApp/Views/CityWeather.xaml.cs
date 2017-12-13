using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityWeather : MasterDetailPage
    {
        public CityWeather()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

            if (Device.RuntimePlatform == Device.UWP)
                MasterBehavior = MasterBehavior.Popover;
        }

        async private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is CityWeatherMenuItem item)
            {
                if(item.TargetType == typeof(CurrentConditions) && Helpers.Settings.MyCitiesList.Count == 0)
                {
                    MasterPage.ListView.SelectedItem = null;
                    await DisplayAlert("Alert", "No cities available yet", "OK");
                    return;
                }
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;

                Detail = new NavigationPage(page);
                IsPresented = false;

                MasterPage.ListView.SelectedItem = null;
            }
        }
    }
}