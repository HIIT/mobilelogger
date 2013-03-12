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

        public override void SaveSensorLog()
        {
            JObject obj = new JObject();
            obj.Add("sessionStart", DeviceTools.GetUnixTime(start));
            obj.Add("sessionEnd", DeviceTools.GetUnixTime(end));
            SaveLogToDB(obj, "/log/session");          
        }

        public void Start()
        {
            System.Diagnostics.Debug.WriteLine("session start");
            this.start = DateTime.UtcNow;
        }

        public void End()
        {
            System.Diagnostics.Debug.WriteLine("session end");
            this.end = DateTime.UtcNow;
            SaveSensorLog();
        }
    }
}
