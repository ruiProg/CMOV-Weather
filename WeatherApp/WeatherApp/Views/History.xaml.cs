using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;



namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class History : ContentPage
    {


        public History(String City, DateTime date)
        {
            InitializeComponent();
            DateTime aDay = DateTime.Now;

            var entries = new List<Microcharts.Entry>
             {
                new Microcharts.Entry(200)
                {
                    Label = "Jan",
                    ValueLabel = "200",
                    Color = SKColor.Parse("#266489")
                },
                new Microcharts.Entry(400)
                {
                Label = "Feb",
                ValueLabel = "400",
                Color = SKColor.Parse("#68B9C0")
                },
                new Microcharts.Entry(-100)
                {
                Label = "Mar",
                ValueLabel = "100",
                Color = SKColor.Parse("#90D585")
                }
            };
            

            var chart = new LineChart() { Entries = entries ,
            LineMode = LineMode.Straight,
            LineSize = 6,
            PointMode = PointMode.Circle,
            PointSize = 12
            };
            

            chartView.Chart = chart;
            /*
            Content = new Microcharts.ChartView
            {
                Text = formatDate(aDay),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };*/
        }

        public String formatDate(DateTime date)
        {
            return date.Year + "-" + date.Month + "-" + date.Day;

        }
    }
}