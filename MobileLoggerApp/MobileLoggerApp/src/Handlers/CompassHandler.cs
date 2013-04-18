using Microsoft.Devices.Sensors;
using MobileLoggerScheduledAgent.Devicetools;
using System;

namespace MobileLoggerApp.Handlers
{
    public class CompassHandler : AbstractLogHandler
    {
        Compass compassWatcher;

        public CompassHandler()
        {
            this.compassWatcher = new Compass();
        }

        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, "/log/compass");
        }

        public override void StartWatcher()
        {
            if (Microsoft.Devices.Environment.DeviceType != Microsoft.Devices.DeviceType.Emulator)
            {
                this.compassWatcher.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                this.compassWatcher.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<CompassReading>>(compass_CurrentValueChanged);
                this.compassWatcher.Start();
                this.IsEnabled = true;
            }
        }

        public override void StopWatcher()
        {
            this.compassWatcher.Stop();
            this.IsEnabled = false;
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
