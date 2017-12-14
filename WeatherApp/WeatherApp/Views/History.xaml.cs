using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;
using WeatherApp.Models;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class History : ContentPage
    {
        public HistoryInfo historyInfo;

        public History(HistoryInfo historyInfo)
        {
            InitializeComponent();
            this.historyInfo = historyInfo;

            generateChartAsync();
    /*
            Content = new Microcharts.ChartView
            {
                Text = formatDate(aDay),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };*/
        }


        private async void generateChartAsync()
        {
            if(this.historyInfo.Hours.Count() != 24)
            {
                await DisplayAlert("Alert", "Error in request", "OK");
            }
            else
            {
                List<float> values = new List<float>();
                String toAdd = "";

                for (int i = 0; i < this.historyInfo.Hours.Count(); i=i+3)
                {
                    if (Helpers.Settings.GetUnit(Unit.tempUnit))
                    {
                        values.Add(historyInfo.Hours[i].TempFahr);
                        toAdd = "°F";
                    }
                    else
                    {
                        values.Add(historyInfo.Hours[i].TempCelsisus);
                        toAdd = "°C";
                    }
                        

                }

                var entries = new List<Microcharts.Entry>();
                for (int i = 0; i < values.Count; i++)
                {
                   var entry = new Microcharts.Entry(values[i])
                    {
                        Label = (i * 3).ToString() + ":00",
                        ValueLabel = values[i].ToString() + toAdd,
                        Color = SKColor.Parse("#266489")
                    };
                    entries.Add(entry);
                }

                var chart = new LineChart()
                {
                    Entries = entries,
                    LineMode = LineMode.Straight,
                    LineSize = 6,
                    PointMode = PointMode.Circle,
                    ValueLabelOrientation = Orientation.Horizontal,
                    LabelTextSize = 25,
                    LabelOrientation = Orientation.Horizontal,
                    MaxValue = 40,
                    MinValue = -5,

                    PointSize = 12
                };


                chartView.Chart = chart;


            }
        }

       
    }
}