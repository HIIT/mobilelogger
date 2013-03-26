using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger
{
    public abstract class AbstractLogHandler
    {
        private LogEventSaver saver = LogEventSaver.Instance;

        protected JObject data = new JObject();

        /// <summary>
        /// This method needs to save the timestamp of current time also
        /// </summary>
        public abstract void SaveSensorLog();

        protected Boolean SaveLogToDB(JObject logEvent, string url)
        {
            if (logEvent["phoneId"] == null)
                logEvent.Add("phoneId", DeviceTools.GetDeviceId());

            if (logEvent["checksum"] != null)
                logEvent.Remove("checksum");
            logEvent.Add("checksum", DeviceTools.CalculateSHA1(logEvent.ToString()));

            saver.addEvent(new JObject(logEvent), url);

            return true;
        }

        protected void AddJOValue(String key, JToken value)
        {
            if (this.data[key] == null)
                this.data.Add(key, value);
            else
                this.data[key].Replace(value);
        }
    }
}
