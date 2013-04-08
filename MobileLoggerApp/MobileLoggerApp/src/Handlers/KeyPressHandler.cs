using MobileLoggerScheduledAgent.Devicetools;
using System.Windows.Input;

namespace MobileLoggerApp.Handlers
{
    public class KeyPressHandler : AbstractLogHandler
    {
        private static string URL = "/log/keyPress";

        public KeyPressHandler()
        {
            this.IsEnabled = true;
        }

        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, URL);
        }

        public void StartKeyPressWatcher()
        {
            MobileLoggerApp.MainPage.keyUp += new MobileLoggerApp.MainPage.KeyPressEventHandler(SearchTextBox_KeyUp);
        }

        public void StopKeyPressWatcher()
        {
            MobileLoggerApp.MainPage.keyUp -= SearchTextBox_KeyUp;
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            AddJOValue("keyPressed", e.Key.ToString());
            AddJOValue("timestamp", DeviceTools.GetUnixTime());
        }
    }
}
