using MobileLoggerApp.src.mobilelogger.Handlers;
using Newtonsoft.Json.Linq;
using System;
using System.Device.Location;
using System.IO;
using System.Net;

namespace MobileLoggerApp.src
{
    class WeatherInformation
    {
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
                    string apiKey = "b531d9d0b8112733132602";

                    string uri = String.Format("http://free.worldweatheronline.com/feed/weather.ashx?q=" + latitude + "," + longitude + "&format=json&num_of_days=2&key=" + apiKey);

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
                System.Diagnostics.Debug.WriteLine(data.Length);
                JObject JSON = JObject.Parse(data);
            }
            catch (Exception exception)
            {
                data = null;
                System.Diagnostics.Debug.WriteLine("{0}, {1} exception at WeatherInformation.GetResponseCallback", exception.Message, exception.StackTrace);
            }
        }
    }
}
