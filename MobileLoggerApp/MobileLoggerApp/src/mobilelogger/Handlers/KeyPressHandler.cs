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
            MobileLoggerApp.MainPage.keyDown += new MobileLoggerApp.MainPage.KeyPressEventHandler(SearchTextBox_KeyDown);
            MobileLoggerApp.MainPage.keyUp += new MobileLoggerApp.MainPage.KeyPressEventHandler(SearchTextBox_KeyUp);

            if (joKeyPress == null)
            {
                joKeyPress = new JObject();
            }
        }

        void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
        }

        void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            AddJOValue("keyPressed", e.PlatformKeyCode);
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
