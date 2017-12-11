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
    public partial class Settings : ContentPage
    {
        private ObservableCollection<Unit> Units { get; set; }

        public Settings()
        {
            Units = new ObservableCollection<Unit>();
            InitializeComponent();
            settingsView.ItemsSource = Units;
            foreach (var entry in Unit.dictionary)
            {
                var currStr = Helpers.Settings.GetUnit(entry.Value.Item3) ? entry.Value.Item2 : entry.Value.Item1;
                Units.Add(new Unit() { Name = entry.Key, Current = currStr });
            }
            if (Device.RuntimePlatform == Device.Android)
                BackgroundColor = Color.FromHex("#404040");
        }

        async void OnTap(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            var item = (Unit)e.Item;
            var value = Unit.dictionary[item.Name];
            var newVal = await DisplayActionSheet(item.Name, "Cancel", null, value.Item1, value.Item2);
            if (item.Current != newVal)
            {
                item.Current = newVal;
                Helpers.Settings.SetUnit(value.Item3, newVal == value.Item2);
            }
        }
    }
}