using Microsoft.Xna.Framework.Audio;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class MicrophoneHandler : AbstractLogHandler
    {
        Microphone microphone;
        JObject joMicro;

        public override void SaveSensorLog()
        {
            SaveLogToDB(joMicro, "/log/micro");
        }
    }
}
