using System;
using System.Windows.Input;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class KeyPressHandler : AbstractLogHandler
    {
        private static string URL = "/log/keyPress";

        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, URL);
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
