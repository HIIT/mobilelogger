using System.Windows.Input;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    class ScreenTouchHandler : AbstractLogHandler
    {
        public override void SaveSensorLog()
        {
            //handle saving in the event handler method below
        }

        public void StartScreenTouchWatcher()
        {
            MobileLoggerApp.MainPage.screenTouch += new MobileLoggerApp.MainPage.TouchEventHandler(Touch_FrameReported);
        }

        void Touch_FrameReported(MainPage mainPage, TouchFrameEventArgs e)
        {
            foreach (TouchPoint touchPoint in e.GetTouchPoints(mainPage))
            {
                this.data = new Newtonsoft.Json.Linq.JObject();
                AddJOValue("xcoord", touchPoint.Position.X);
                AddJOValue("ycoord", touchPoint.Position.Y);
                AddJOValue("action", touchPoint.Action.ToString());
                AddJOValue("timestamp", DeviceTools.GetUnixTime());
                SaveLogToDB(this.data, "/log/touch");
            }
        }
    }
}
