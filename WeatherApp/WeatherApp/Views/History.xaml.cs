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
                        Color = SKColor.Parse(getColor(i*3))
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

        private String getColor(int i)
        {
            String returnValue = "";
            switch (i)
            {
                case 0:
                    returnValue = "#000000";
                    break;
                case 3:
                    returnValue = "#424100";
                    break;
                case 6:
                    returnValue = "#6d6c01";
                    break;
                case 9:
                    returnValue = "#d6d302";
                    break;
                case 12:
                    returnValue = "#fffa00";
                    break;
                case 15:
                    returnValue = "#e0dd00";
                    break;
                case 18:
                    returnValue = "#a09e00";
                    break;
                case 21:
                    returnValue = "#605f00";
                    break;
                default:
                    returnValue = "#266489";
                    break;
            }

            return returnValue;
        }




    }
}