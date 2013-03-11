using MobileLoggerScheduledAgent;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger
{
    public abstract class AbstractLogHandler
    {
        public abstract void SaveSensorLog();

        protected Boolean SaveLogToDB(JObject logEvent, string url)
        {
            if (logEvent["phoneId"] == null)
                logEvent.Add("phoneId", DeviceTools.GetDeviceId());
            

            if (logEvent["timestamp"] != null)
                logEvent.Remove("timestamp");
            
            logEvent.Add("timestamp", DeviceTools.GetUnixTime(DateTime.Now));

            if (logEvent["checksum"] != null)
                logEvent.Remove("checksum");
            logEvent.Add("checksum", DeviceTools.CalculateSHA1(logEvent.ToString()));
            
            using (LogEventDataContext logDBContext = new LogEventDataContext(MainPage.ConnectionString))
            {
                if (!logDBContext.DatabaseExists()) return false;

                logDBContext.addEvent(logEvent.ToString(), url);
            }
            return true;
        }
    }
}
