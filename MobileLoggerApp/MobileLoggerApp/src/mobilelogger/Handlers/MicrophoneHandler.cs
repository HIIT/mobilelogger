using Microsoft.Xna.Framework.Audio;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class MicrophoneHandler : AbstractLogHandler
    {
        Microphone microphone;
        JObject joMicro;

        DateTime lastSaved;

        public override void SaveSensorLog()
        {
            if (DeviceTools.SensorLastSavedTimeSpan(lastSaved))
            {
                SaveLogToDB(joMicro, "/log/micro");
                lastSaved = DateTime.UtcNow;
            }
        }
    }
}
