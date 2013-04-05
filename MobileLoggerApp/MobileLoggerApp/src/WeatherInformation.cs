using MobileLoggerApp.Handlers;
using MobileLoggerScheduledAgent.Devicetools;
using Newtonsoft.Json.Linq;
using System;
using System.Device.Location;
using System.IO;
using System.Net;

namespace MobileLoggerApp
{
    class WeatherInformation
    {
        public delegate void WeatherDataHandler(JObject weatherData);
        public static event WeatherDataHandler weatherDataEvent;

        public WeatherInformation()
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
                    string uri = String.Format("http://api.worldweatheronline.com/free/v1/weather.ashx?q=" + latitude + "," + longitude + "&format=json&num_of_days=1&key=" + DeviceTools.worldWeatherOnlineApiKey);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                    request.Method = "GET";
                    request.BeginGetResponse(GetResponseCallback, request);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="asynchronousResult">Asynchronous result of the former http request object</param>
        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            request.BeginGetResponse(GetResponseCallback, request);
        }

        /// <summary>
        /// </summary>
        /// <param name="ar">Asynchronous result of the former http request object</param>
        private void GetResponseCallback(IAsyncResult ar)
        {
            HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
            string data;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
                var stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                data = reader.ReadToEnd();
                JObject weatherData = JObject.Parse(data);
                weatherDataEvent(weatherData);
            }
            catch (Exception exception)
            {
                data = null;
                System.Diagnostics.Debug.WriteLine("{0}, {1} exception at WeatherInformation.GetResponseCallback", exception.Message, exception.StackTrace);
            }
        }
    }
}
