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
            WeatherInformation.weatherDataEvent += new WeatherInformation.WeatherDataHandler(WeatherData);
        }

        private void WeatherData(JObject weatherData)
        {
            DateTime timestamp = DateTime.UtcNow;
            weatherData.Add("time", DeviceTools.GetUnixTime(timestamp));
            SaveLogToDB(weatherData, "/log/weather");
        }
    }
}
