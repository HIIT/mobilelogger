using Newtonsoft.Json.Linq;

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
