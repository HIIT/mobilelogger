﻿using System.Windows.Input;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class KeyPressHandler : AbstractLogHandler
    {
        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, "/log/keyPress");
        }

        public void StartKeyPressWatcher()
        {
            MobileLoggerApp.MainPage.keyUp += new MobileLoggerApp.MainPage.KeyPressEventHandler(SearchTextBox_KeyUp);
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            AddJOValue("keyPressed", e.Key.ToString());
            AddJOValue("timestamp", DeviceTools.GetUnixTime());
        }
    }
}
