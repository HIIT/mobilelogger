using Newtonsoft.Json.Linq;
using System.Windows.Input;

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
            MobileLoggerApp.MainPage.keyDown += new MobileLoggerApp.MainPage.KeyPressEventHandler(SearchTextBox_KeyDown);
            MobileLoggerApp.MainPage.keyUp += new MobileLoggerApp.MainPage.KeyPressEventHandler(SearchTextBox_KeyUp);
        }

        void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
        }

        void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            AddJOValue("keyPressed", e.PlatformKeyCode);
        }
    }
}
