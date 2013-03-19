using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    class KeyboardHandler : AbstractLogHandler
    {
        JObject joKeyboard;

        public override void SaveSensorLog()
        {
            SaveLogToDB(joKeyboard, "/log/keyboard");
        }

        public void StartKeyBoardWatcher()
        {
            MobileLoggerApp.MainPage.keyboardGotFocus += new MobileLoggerApp.MainPage.KeyboardFocus(KeyboardGotFocus);
            MobileLoggerApp.MainPage.keyboardLostFocus += new MobileLoggerApp.MainPage.KeyboardFocus(KeyboardLostFocus);

            if (joKeyboard == null)
            {
                joKeyboard = new JObject();
            }
        }

        void KeyboardGotFocus()
        {
            AddJOValue("keyboardFocus", "gotFocus");
        }

        void KeyboardLostFocus()
        {
            AddJOValue("keyboardFocus", "lostFocus");
        }

        private void AddJOValue(String key, String value)
        {
            if (joKeyboard[key] == null)
            {
                joKeyboard.Add(key, value);
            }
            else
            {
                joKeyboard[key].Replace(value);
            }
        }
    }
}