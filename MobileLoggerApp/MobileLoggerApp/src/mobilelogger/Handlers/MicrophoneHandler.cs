using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
