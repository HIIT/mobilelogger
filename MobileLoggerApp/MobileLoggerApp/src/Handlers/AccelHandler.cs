using Microsoft.Devices.Sensors;
using MobileLoggerScheduledAgent.Devicetools;
using System;

namespace MobileLoggerApp.Handlers
{
    public class AccelHandler : AbstractLogHandler
    {
        Accelerometer accelerometerWatcher;

        public AccelHandler()
        {
            this.accelerometerWatcher = new Accelerometer();
        }

        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, "/log/accel");
        }

        public override void StartWatcher()
        {
            this.accelerometerWatcher.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
            this.accelerometerWatcher.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(accelerometer_CurrentValueChanged);
            this.accelerometerWatcher.Start();
            this.IsEnabled = true;
        }

        public override void StopWatcher()
        {
            this.accelerometerWatcher.Stop();
            this.IsEnabled = false;
        }

        void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            AddJOValue("accX", e.SensorReading.Acceleration.X);
            AddJOValue("accY", e.SensorReading.Acceleration.Y);
            AddJOValue("accZ", e.SensorReading.Acceleration.Z);
            AddJOValue("timestamp", DeviceTools.GetUnixTime());
        }
    }
}
