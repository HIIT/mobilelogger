using Newtonsoft.Json.Linq;
using System;
using System.Windows.Input;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class KeyPressHandler : AbstractLogHandler
    {
        JObject joKeyPress;

        public override void SaveSensorLog()
        {
            SaveLogToDB(joKeyPress, "/log/keyPress");
        }

        public void StartKeyPressWatcher()
        {
            MobileLoggerApp.MainPage.keyPress += new MobileLoggerApp.MainPage.KeyPressEventHandler(SearchTextBox_KeyPress);

            if (joKeyPress == null)
            {
                joKeyPress = new JObject();
            }
        }

        void SearchTextBox_KeyPress(object sender, KeyEventArgs e, Boolean press)
        {
            if (press)
            {
            }
            else
            {
                AddJOValue("keyPressed", e.PlatformKeyCode);
            }
        }

        private void AddJOValue(String key, int value)
        {
            if (joKeyPress[key] == null)
            {
                joKeyPress.Add(key, value);
            }
            else
            {
                joKeyPress[key].Replace(value);
            }
        }
    }
}
