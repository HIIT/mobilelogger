using System.Windows.Input;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    class ScreenTouchHandler : AbstractLogHandler
    {
        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, "/log/touch");
        }

        public void StartScreenTouchWatcher()
        {
            MobileLoggerApp.MainPage.screenTouch += new MobileLoggerApp.MainPage.TouchEventHandler(Touch_FrameReported);
        }

        void Touch_FrameReported(MainPage mainPage, TouchFrameEventArgs e)
        {
            foreach (TouchPoint touchPoint in e.GetTouchPoints(mainPage))
            {
                if (touchPoint.Action == TouchAction.Down)
                {
                    AddJOValue("touchPointDownX", touchPoint.Position.X);
                    AddJOValue("touchPointDownY", touchPoint.Position.Y);
                    AddJOValue("timestamp", DeviceTools.GetUnixTime());
                }
                else if (touchPoint.Action == TouchAction.Move && e.GetPrimaryTouchPoint(mainPage) != null)
                {
                    if (touchPoint.TouchDevice.Id == e.GetPrimaryTouchPoint(mainPage).TouchDevice.Id)
                    {
                    }
                }
                else if (touchPoint.Action == TouchAction.Up)
                {
                    AddJOValue("touchPointUpX", touchPoint.Position.X);
                    AddJOValue("touchPointUpY", touchPoint.Position.Y);
                    AddJOValue("timestamp", DeviceTools.GetUnixTime());
                }
            }
        }
    }
}
