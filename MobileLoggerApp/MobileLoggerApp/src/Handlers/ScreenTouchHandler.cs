using MobileLoggerScheduledAgent.Devicetools;
using System.Windows.Input;

namespace MobileLoggerApp.Handlers
{
    class ScreenTouchHandler : AbstractLogHandler
    {
        public ScreenTouchHandler()
        {
        }

        public override void SaveSensorLog()
        {
            //handle saving in the event handler method below
        }

        public override void StartWatcher()
        {
            MobileLoggerApp.MainPage.screenTouch += new MobileLoggerApp.MainPage.TouchEventHandler(Touch_FrameReported);
            this.IsEnabled = true;
        }

        public override void StopWatcher()
        {
            MobileLoggerApp.MainPage.screenTouch -= Touch_FrameReported;
            this.IsEnabled = false;
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
