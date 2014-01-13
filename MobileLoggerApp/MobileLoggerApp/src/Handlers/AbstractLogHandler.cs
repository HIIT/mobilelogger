using MobileLoggerScheduledAgent.Devicetools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.Handlers
{
    public abstract class AbstractLogHandler
    {
        protected JObject data = new JObject();
        public bool IsEnabled { get; set; }

        /// <summary>
        /// This method needs to save the timestamp of current time also
        /// </summary>
        public abstract void SaveSensorLog();
        public abstract void StartWatcher();
        public abstract void StopWatcher();

        private readonly DateTimeOffset epoc = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        protected int UnixTime(DateTimeOffset time)
        {
            return (int)( (time - epoc).TotalSeconds );
        }

        protected Boolean SaveLogToDB(JObject jsonLog, string url)
        {
            if (!IsEnabled)
                return true;

            if (jsonLog == null)
                return false;

            if (jsonLog["phoneId"] == null)
                jsonLog.Add("phoneId", DeviceTools.GetDeviceId());

            if (jsonLog["checksum"] != null)
                jsonLog.Remove("checksum");
            jsonLog.Add("checksum", DeviceTools.CalculateSHA1(jsonLog.ToString(Formatting.None)));

            if (jsonLog.ToString(Formatting.None).Length <= 4000)
                LogEventSaver.Instance.addEvent(jsonLog.ToString(Formatting.None), url);

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
