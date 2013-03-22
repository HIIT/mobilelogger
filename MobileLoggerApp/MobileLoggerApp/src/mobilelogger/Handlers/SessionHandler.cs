using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Text;

namespace MobileLoggerApp.src.mobilelogger
{
    public class SessionHandler : AbstractLogHandler
    {
        private DateTime start;
        private DateTime end;

        private static bool hasEnded = false;
        private static bool hasStarted = false;

        /// <summary>
        /// Use End() instead
        /// </summary>
        public override void SaveSensorLog()
        {
        }

        public void Start()
        {
            System.Diagnostics.Debug.WriteLine("session start");
            this.start = DateTime.UtcNow;
            hasStarted = true;
        }

        public void End()
        {
            System.Diagnostics.Debug.WriteLine("session end");
            this.end = DateTime.UtcNow;
            hasEnded = true;

            JObject obj = new JObject();
            obj.Add("sessionStart", DeviceTools.GetUnixTime(start));
            obj.Add("sessionEnd", DeviceTools.GetUnixTime(end));

            if (hasStarted && hasEnded)
            {
                SaveLogToDB(obj, "/log/session");
                hasStarted = hasEnded = false;
                LogEventSaver.Instance.SaveAll();
            }
        }
    }
}
