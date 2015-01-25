using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorldWeatherOnline.Client.Models;

namespace WorldWeatherOnline.Client
{
    /// <summary>
    /// The client object to access the WorldWeatherOnline API
    /// </summary>
    public class WorldWeatherOnlineClient
    {
        private const string GeolookupAndCurrentConditionsUri = "http://api.worldweatheronline.com/free/v2/weather.ashx?key={0}&q={1},{2}&format=json&num_of_days=1";
        private const string GeolookupCurrentConditionsAndForecastUri = "http://api.wunderground.com/api/{0}/geolookup/conditions/forecast/q/{1},{2}.json";
        private const string GeolookupHourlyForecastUri = "http://api.wunderground.com/api/27d9503963b27155/geolookup/hourly/q/{1},{2}.json";

        /// <summary>
        /// Gets the current conditions for the specified coordinates
        /// </summary>
        /// <param name="lat">The latitude</param>
        /// <param name="lng">The longitude</param>
        /// <returns>The response object</returns>
        public static async Task<WeatherResponse> GetConditionsForLocationAsync(double lat, double lng)
        {
            string uri = string.Format(GeolookupAndCurrentConditionsUri, Config.ApiKey, lat, lng);

            using (var client = new HttpClient())
            {
                HttpResponseMessage response =  await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(content);

                    if (weatherResponse.results != null && weatherResponse.results.error != null)
                        throw new WeatherServiceException(weatherResponse.results.error.message);

                    return weatherResponse;
                }
            }
            return null;
        }

        /// <summary>
        /// The configuration for the WorldWeatherOnline Client
        /// </summary>
        public static class Config
        {
            /// <summary>
            /// The API Key for the WorldWeatherOnline API. Get yours at http://www.worldweatheronline.com
            /// </summary>
            public static string ApiKey { get; set; }
        }
    }

    /// <summary>
    /// An exception thrown by the weather service
    /// </summary>
    public class WeatherServiceException : Exception
    {
        /// <summary>
        /// Creates a new exception with the specified message
        /// </summary>
        /// <param name="message">The message</param>
        public WeatherServiceException(string message) : base(message) { }
    }
}
