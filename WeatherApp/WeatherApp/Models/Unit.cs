using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    class Unit : INotifyPropertyChanged
    {
        public static string tempUnit = "temp_unit";
        public static string windUnit = "wind_unit";
        public static string precUnit = "prec_unit";
        public static string visUnit = "vis_unit";
        String current;

        public event PropertyChangedEventHandler PropertyChanged;

        public static Dictionary<string, Tuple<string, string, string>> dictionary = new Dictionary<string, Tuple<string, string, string>>()
        {
            { "Temperature Unit", Tuple.Create("Celsius", "Fahrenheit", tempUnit) },
            { "Wind Speed Unit", Tuple.Create("Kilometers Per Hour", "Miles Per Hour", windUnit) },
            { "Precipition Unit", Tuple.Create("Millimeters", "Inches", precUnit) },
            { "Visibility Unit", Tuple.Create("Kilometers", "Miles", visUnit) }
        };

        public string Name { get; set; }

        public string Current
        {
            set
            {
                if (current != value)
                {
                    current = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Current"));
                    }
                }
            }
            get
            {
                return current;
            }
        }

        public Unit()
        {
            Name = "";
            Current = "";
        }
    }
}
