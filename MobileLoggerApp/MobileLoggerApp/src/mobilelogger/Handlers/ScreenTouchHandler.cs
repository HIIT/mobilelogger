using System.Windows.Input;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    class ScreenTouchHandler : AbstractLogHandler
    {
        public override void SaveSensorLog()
        {
            //handle saving in the event handler method below
            ///SaveLogToDB(this.data, "/log/touch");
        }

        public void StartScreenTouchWatcher()
        {
            MobileLoggerApp.MainPage.screenTouch += new MobileLoggerApp.MainPage.TouchEventHandler(Touch_FrameReported);
        }

        void Touch_FrameReported(MainPage mainPage, TouchFrameEventArgs e)
        {
            foreach (TouchPoint touchPoint in e.GetTouchPoints(mainPage))
            {
               
                if (touchPoint.Action == TouchAction.Move && e.GetPrimaryTouchPoint(mainPage) != null)
                {
                    if (touchPoint.TouchDevice.Id == e.GetPrimaryTouchPoint(mainPage).TouchDevice.Id)
                    {
                       
                    }
                }
                else 
                {
                    this.data = new Newtonsoft.Json.Linq.JObject();
                    AddJOValue("X", touchPoint.Position.X);
                    AddJOValue("Y", touchPoint.Position.Y);
                    AddJOValue("Action", touchPoint.Action.ToString());
                    AddJOValue("timestamp", DeviceTools.GetUnixTime());
                    SaveLogToDB(this.data, "/log/touch");
                }
            }
        }
    }
}
