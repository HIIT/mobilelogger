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

        public override void StartWatcher()
        {
            WeatherInformationSearch.weatherDataEvent += new WeatherInformationSearch.WeatherDataHandler(WeatherData);
        }

        public override void StopWatcher()
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
