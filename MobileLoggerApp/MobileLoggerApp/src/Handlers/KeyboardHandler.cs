﻿
using MobileLoggerScheduledAgent.Devicetools;

namespace MobileLoggerApp.Handlers
{
    class KeyboardHandler : AbstractLogHandler
    {
        private static string URL = "/log/keyboard";

        public KeyboardHandler()
        {
        }
        
        public override void SaveSensorLog()
        {
            //handle saving in the event handler method below
        }

        public override void StartWatcher()
        {
            MobileLoggerApp.MainPage.keyboardGotFocus += new MobileLoggerApp.MainPage.KeyboardFocusHandler(KeyboardGotFocus);
            MobileLoggerApp.MainPage.keyboardLostFocus += new MobileLoggerApp.MainPage.KeyboardFocusHandler(KeyboardLostFocus);
            this.IsEnabled = true;
        }

        public override void StopWatcher()
        {
            MobileLoggerApp.MainPage.keyboardGotFocus -= KeyboardGotFocus;
            MobileLoggerApp.MainPage.keyboardLostFocus -= KeyboardLostFocus;
            this.IsEnabled = false;
        }

        private void KeyboardGotFocus()
        {
            this.data = new Newtonsoft.Json.Linq.JObject();
            AddJOValue("keyboardFocus", "gotFocus");
            AddJOValue("timestamp", DeviceTools.GetUnixTime());
            SaveLogToDB(this.data, URL);
        }

        private void KeyboardLostFocus()
        {
            this.data = new Newtonsoft.Json.Linq.JObject();
            AddJOValue("keyboardFocus", "lostFocus");
            AddJOValue("timestamp", DeviceTools.GetUnixTime());
            SaveLogToDB(this.data, URL);
        }
    }
}
