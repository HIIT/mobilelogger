using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    class KeyboardHandler : AbstractLogHandler
    {

        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, "/log/keyboard");
        }

        public void StartKeyBoardWatcher()
        {
            MobileLoggerApp.MainPage.keyboardGotFocus += new MobileLoggerApp.MainPage.KeyboardFocus(KeyboardGotFocus);
            MobileLoggerApp.MainPage.keyboardLostFocus += new MobileLoggerApp.MainPage.KeyboardFocus(KeyboardLostFocus);

            if (this.data == null)
            {
                this.data = new JObject();
            }
        }

        void KeyboardGotFocus()
        {
            AddJOValue("keyboardFocus", "gotFocus");
            AddJOValue("timestamp", DeviceTools.GetUnixTime());
        }

        void KeyboardLostFocus()
        {
            AddJOValue("keyboardFocus", "lostFocus");
            AddJOValue("timestamp", DeviceTools.GetUnixTime());
        }
    }
}