using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            MobileLoggerApp.MainPage.keyboardFocus += new MobileLoggerApp.MainPage.KeyboardVisible(KeyboardVisible);

            if (joKeyboard == null)
            {
                joKeyboard = new JObject();
            }
        }

        void KeyboardVisible(Boolean focus)
        {
            AddJOValue("keyboardVisible", focus);
        }

        private void AddJOValue(String key, Boolean value)
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