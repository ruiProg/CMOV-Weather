﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{

    public class CityWeatherMenuItem
    {
        public CityWeatherMenuItem()
        {
            TargetType = typeof(CityWeatherDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}