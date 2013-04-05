using MobileLoggerApp.Handlers;
using MobileLoggerScheduledAgent.Devicetools;
using Newtonsoft.Json.Linq;
using System;
using System.Device.Location;

namespace MobileLoggerApp
{
    class WeatherInformationSearch : HttpRequestable
    {
        public delegate void WeatherDataHandler(JObject weatherData);
        public static event WeatherDataHandler weatherDataEvent;

        public WeatherInformationSearch()
        {
        }

        /// <summary> 
        /// Get a forecast for the given latitude and longitude 
        /// </summary> 
        public void GetForecast()
        {
            if (GpsHandler.coordinateWatcher.Permission != GeoPositionPermission.Denied)
            {
                if (GpsHandler.coordinateWatcher.Status == GeoPositionStatus.Ready)
                {
                    string latitude = GpsHandler.latitude;
                    string longitude = GpsHandler.longitude;
                    int days = 1;
                    string uri = String.Format("http://api.worldweatheronline.com/free/v1/weather.ashx?q=" + latitude + "," + longitude + "&format=json&num_of_days=" + days + "&key=" + DeviceTools.worldWeatherOnlineApiKey);

                    HttpRequest request = new HttpRequest(uri, this);
                }
            }
        }

        public void Callback(string data)
        {
            JObject weatherData = JObject.Parse(data);
            weatherDataEvent(weatherData);
        }

        public void HandleRequestError(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine("{0}, {1} exception at WeatherInformationSearch.GetResponseCallback", exception.Message, exception.StackTrace);
        }
    }
}
