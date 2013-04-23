using MobileLoggerScheduledAgent.Devicetools;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.Handlers
{
    public class SessionHandler : AbstractLogHandler
    {
        private DateTime start, end;

        private static bool hasEnded = false;
        private static bool hasStarted = false;

        /// <summary>
        /// Use End() instead
        /// </summary>
        public override void SaveSensorLog()
        {
            //handle saving in the event handler method below
        }

        public override void StartWatcher()
        {
        }

        public override void StopWatcher()
        {
        }

        public void Start()
        {
            this.start = DateTime.UtcNow;
            hasStarted = true;
        }

        public void End()
        {
            this.end = DateTime.UtcNow;
            hasEnded = true;

            JObject obj = new JObject();
            obj.Add("sessionStart", DeviceTools.GetUnixTime(start));
            obj.Add("sessionEnd", DeviceTools.GetUnixTime(end));

            if (hasStarted && hasEnded)
            {
                SaveLogToDB(obj, "/log/session");
                hasStarted = hasEnded = false;
            }
        }
    }
}
