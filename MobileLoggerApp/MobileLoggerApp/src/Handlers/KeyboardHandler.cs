
using MobileLoggerScheduledAgent.Devicetools;

namespace MobileLoggerApp.Handlers
{
    class KeyboardHandler : AbstractLogHandler
    {
        private static string URL = "/log/keyboard";

        public KeyboardHandler()
        {
            this.IsEnabled = true;
        }
        
        public override void SaveSensorLog()
        {
            //handle saving in the event handler method below
        }

        public override void StartWatcher()
        {
            MobileLoggerApp.MainPage.keyboardGotFocus += new MobileLoggerApp.MainPage.KeyboardFocusHandler(KeyboardGotFocus);
            MobileLoggerApp.MainPage.keyboardLostFocus += new MobileLoggerApp.MainPage.KeyboardFocusHandler(KeyboardLostFocus);
        }

        public override void StopWatcher()
        {
            MobileLoggerApp.MainPage.keyboardGotFocus -= new MobileLoggerApp.MainPage.KeyboardFocusHandler(KeyboardGotFocus);
            MobileLoggerApp.MainPage.keyboardGotFocus -= KeyboardGotFocus;
            MobileLoggerApp.MainPage.keyboardLostFocus -= new MobileLoggerApp.MainPage.KeyboardFocusHandler(KeyboardLostFocus);
            MobileLoggerApp.MainPage.keyboardLostFocus -= KeyboardLostFocus;
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
