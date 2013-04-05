using MobileLoggerScheduledAgent.Devicetools;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.Handlers
{
    class WeatherDataHandler : AbstractLogHandler
    {
        public override void SaveSensorLog()
        {
        }

        public void StartWeatherDataHandler()
        {
            WeatherInformationSearch.weatherDataEvent += new WeatherInformationSearch.WeatherDataHandler(WeatherData);
        }

        private void WeatherData(JObject weatherData)
        {
            DateTime timestamp = DateTime.UtcNow;
            weatherData.Add("time", DeviceTools.GetUnixTime(timestamp));
            SaveLogToDB(weatherData, "/log/weather");
        }
    }
}
