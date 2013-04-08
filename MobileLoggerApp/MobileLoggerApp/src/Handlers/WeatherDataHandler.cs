using MobileLoggerScheduledAgent.Devicetools;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.Handlers
{
    class WeatherDataHandler : AbstractLogHandler
    {
        public WeatherDataHandler()
        {
            this.IsEnabled = true;            
        }

        public override void SaveSensorLog()
        {
        }

        public void StartWeatherDataWatcher()
        {
            WeatherInformationSearch.weatherDataEvent += new WeatherInformationSearch.WeatherDataHandler(WeatherData);
        }

        public void StopWeatherDataWatcher()
        {
            WeatherInformationSearch.weatherDataEvent -= WeatherData;
        }

        private void WeatherData(JObject weatherData)
        {
            DateTime timestamp = DateTime.UtcNow;
            weatherData.Add("time", DeviceTools.GetUnixTime(timestamp));
            SaveLogToDB(weatherData, "/log/weather");
        }
    }
}
