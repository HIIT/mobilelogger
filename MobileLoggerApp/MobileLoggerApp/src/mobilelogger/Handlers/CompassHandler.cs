using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Devices.Sensors;
using System.Windows.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class CompassHandler : AbstractLogHandler
    {
        Compass compass;
        DispatcherTimer timer;
        JObject joCompass;

        double magneticHeading;
        double trueHeading;
        double headingAccuracy;
        //Vector3 rawMagnetometerReading;
        bool isDataValid;

        bool calibrating = false;

        public override void SaveSensorLog()
        {
            SaveLogToDB(joCompass, "/log/compass");
        }
        public void startCompassWatcher()
        {
            if (compass == null)
            {
                // Instantiate the Compass.
                compass = new Compass();
                //compass.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                compass.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<CompassReading>>(compass_CurrentValueChanged);
                //compass.Calibrate += new EventHandler<CalibrationEventArgs>(compass_Calibrate);
            }

        }
        void compass_CurrentValueChanged(object sender, SensorReadingEventArgs<CompassReading> e)
{

    trueHeading = e.SensorReading.TrueHeading;
    magneticHeading = e.SensorReading.MagneticHeading;
    headingAccuracy = Math.Abs(e.SensorReading.HeadingAccuracy);
    //rawMagnetometerReading = e.SensorReading.MagnetometerReading;
    }

}
}
