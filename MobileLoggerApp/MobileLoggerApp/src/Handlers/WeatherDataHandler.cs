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
            //handle saving in the event handler method below
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
            this.data = weatherData;
            AddJOValue("timestamp", DeviceTools.GetUnixTime());
            SaveLogToDB(this.data, "/log/weather");
        }
    }
}
