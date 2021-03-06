﻿using MobileLoggerScheduledAgent.Devicetools;
using System.Windows.Input;

namespace MobileLoggerApp.Handlers
{
    public class KeyPressHandler : AbstractLogHandler
    {
        private static string URL = "/log/keyPress";

        public KeyPressHandler()
        {
        }

        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, URL);
        }

        public override void StartWatcher()
        {
            MobileLoggerApp.MainPage.keyUp += new MobileLoggerApp.MainPage.KeyPressEventHandler(SearchTextBox_KeyUp);
            this.IsEnabled = true;
        }

        public override void StopWatcher()
        {
            MobileLoggerApp.MainPage.keyUp -= SearchTextBox_KeyUp;
            this.IsEnabled = false;
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            AddJOValue("keyPressed", e.Key.ToString());
            AddJOValue("timestamp", DeviceTools.GetUnixTime());
        }
    }
}
