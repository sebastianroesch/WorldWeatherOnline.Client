using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldWeatherOnline.Client;
using WorldWeatherOnline.Client.Models;

namespace WorldWeatherOnline.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Configure the client by setting the API Key. Get yours at http://www.worldweatheronline.com
            WorldWeatherOnline.Client.WorldWeatherOnlineClient.Config.ApiKey = ConfigurationSettings.AppSettings["ApiKey"];

            //Get the current weather conditions for the specified location
            WeatherResponse current = WorldWeatherOnlineClient.GetConditionsForLocationAsync(53.579724, 10.071984).Result;
            Debug.WriteLine(current.data.current_condition.First().weatherDesc.First().value);

            //Get the weather forecast for the specified location
            //WeatherResponse forecast = WWorldWeatherOnlineClient.GetConditionsAndForecastForLocationAsync(51.4800, 0.0).Result;
            //Debug.WriteLine(forecast.forecast.txt_forecast.forecastday[0].fcttext);
        }
    }
}
