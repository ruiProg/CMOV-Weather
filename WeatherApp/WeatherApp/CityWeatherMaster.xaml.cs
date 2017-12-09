using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityWeatherMaster : ContentPage
    {
        public ListView ListView;

        public CityWeatherMaster()
        {
            InitializeComponent();

            BindingContext = new CityWeatherMasterViewModel();
            ListView = MenuItemsListView;
        }

        class CityWeatherMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<CityWeatherMenuItem> MenuItems { get; set; }

            public CityWeatherMasterViewModel()
            {
                MenuItems = new ObservableCollection<CityWeatherMenuItem>(new[]
                {
                    new CityWeatherMenuItem { Id = 0, Title = "Page 1" },
                    new CityWeatherMenuItem { Id = 1, Title = "Page 2" },
                    new CityWeatherMenuItem { Id = 2, Title = "Page 3" },
                    new CityWeatherMenuItem { Id = 3, Title = "Page 4" },
                    new CityWeatherMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}