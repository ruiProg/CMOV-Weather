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
    public partial class DatepickerModal : ContentPage
    {

        DatePicker datepicker;
        String City;
        public DatepickerModal(String City)
        {
            InitializeComponent();

            this.City = City;

            var label = new Label
            {
                Text = "Date input",
                FontSize = 40,
                FontFamily = "Abril Fatface",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

         



            datepicker = new DatePicker
            {
                VerticalOptions = LayoutOptions.Center,
                Date = DateTime.Now.AddDays(-1),
                MaximumDate = DateTime.Now.AddDays(-1),
                MinimumDate = DateTime.Now.AddDays(-31)
            };

            var dismissButton = new Button
            {
                Text = "Dismiss",
           
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.Red,
                FontSize = 20,
                FontFamily = "Open Sans"

            };
            dismissButton.Clicked += OnDismissButtonClicked;

            var okButton = new Button
            {
                Text = "OK",
               
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.Green,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                FontFamily = "Open Sans"

            };
            okButton.Clicked += OnOkButtonClicked;

            var buttons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    dismissButton,okButton
                }

            };

            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    label, datepicker,buttons
                }
                
            };
        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }

        async void OnOkButtonClicked(object sender, EventArgs args)
        {
            App.currentCity = City;
            HistoryTemp temp = null;
            try
            {
                temp = await Service.getHistoryWeather(formatDate(datepicker.Date));
            }
            catch (Exception)
            {
            }
            if (temp == null)
            {
                temp = new HistoryTemp();
                await DisplayAlert("Alert", "Service is not available", "OK");

            }
            else
            {
                HistoryInfo data = new HistoryInfo(temp);
                var historyPage = new History(data);
                await Navigation.PushAsync(historyPage);
            }
            

        }


        

        public String formatDate(DateTime date)
        {
            return date.Year + "-" + date.Month + "-" + date.Day;

        }
    }
}