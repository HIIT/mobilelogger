using Microsoft.Devices.Sensors;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class CompassHandler : AbstractLogHandler
    {
        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, "/log/compass");
        }
        public void StartCompassWatcher()
        {
            if (Microsoft.Devices.Environment.DeviceType != Microsoft.Devices.DeviceType.Emulator)
            {
                Compass compassWatcher = new Compass();
                compassWatcher.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                compassWatcher.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<CompassReading>>(compass_CurrentValueChanged);
                compassWatcher.Start();
            }
        }

        void compass_CurrentValueChanged(object sender, SensorReadingEventArgs<CompassReading> e)
        {
            AddJOValue("trueHeading", e.SensorReading.TrueHeading);
            AddJOValue("magneticHeading", e.SensorReading.MagneticHeading);
            AddJOValue("headingAccuracy", e.SensorReading.HeadingAccuracy);
            AddJOValue("rawMagneticReadingX", e.SensorReading.MagnetometerReading.X);
            AddJOValue("rawMagneticReadingY", e.SensorReading.MagnetometerReading.Y);
            AddJOValue("rawMagneticReadingZ", e.SensorReading.MagnetometerReading.Z);
            AddJOValue("timestamp", DeviceTools.GetUnixTime());
        }
    }
}
