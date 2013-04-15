using MobileLoggerScheduledAgent.Devicetools;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.Handlers
{
    public abstract class AbstractLogHandler
    {
        //private LogEventSaver saver = LogEventSaver.Instance;

        protected JObject data = new JObject();
        public bool IsEnabled { get; set; }

        /// <summary>
        /// This method needs to save the timestamp of current time also
        /// </summary>
        public abstract void SaveSensorLog();
        public abstract void StartWatcher();
        public abstract void StopWatcher();

        protected Boolean SaveLogToDB(JObject json, string url)
        {
            if (!IsEnabled)
                return true;

            if (json == null)
                return false;

            if (json["phoneId"] == null)
                json.Add("phoneId", DeviceTools.GetDeviceId());

            if (json["checksum"] != null)
                json.Remove("checksum");
            json.Add("checksum", DeviceTools.CalculateSHA1(json.ToString(Newtonsoft.Json.Formatting.None)));

            if (json.ToString(Newtonsoft.Json.Formatting.None).Length <= 4000)
                LogEventSaver.Instance.addEvent(json.ToString(Newtonsoft.Json.Formatting.None), url);


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
