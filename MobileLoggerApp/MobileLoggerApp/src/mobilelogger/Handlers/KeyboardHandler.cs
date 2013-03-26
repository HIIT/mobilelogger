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
            MobileLoggerApp.MainPage.keyboardGotFocus += new MobileLoggerApp.MainPage.KeyboardFocusHandler(KeyboardGotFocus);
            MobileLoggerApp.MainPage.keyboardLostFocus += new MobileLoggerApp.MainPage.KeyboardFocusHandler(KeyboardLostFocus);
        }

        private void KeyboardGotFocus()
        {
            AddJOValue("keyboardFocus", "gotFocus");
            AddJOValue("timestamp", DeviceTools.GetUnixTime());
        }

        private void KeyboardLostFocus()
        {
            AddJOValue("keyboardFocus", "lostFocus");
            AddJOValue("timestamp", DeviceTools.GetUnixTime());
        }
    }
}
