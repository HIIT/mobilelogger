using Microsoft.Devices.Sensors;
using MobileLoggerScheduledAgent.Devicetools;
using System;

namespace MobileLoggerApp.Handlers
{
    public class GyroHandler : AbstractLogHandler
    {
        Gyroscope gyroWatcher;

        public GyroHandler()
        {
            this.gyroWatcher = new Gyroscope();
            this.IsEnabled = true;
        }

        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, "/log/gyro");
        }

        public void StartGyroWatcher()
        {
            if (Microsoft.Devices.Environment.DeviceType != Microsoft.Devices.DeviceType.Emulator)
            {
                this.gyroWatcher.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                this.gyroWatcher.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<GyroscopeReading>>(gyroscope_CurrentValueChanged);
                this.gyroWatcher.Start();
            }
        }

        public void StopGyroWatcher()
        {
            this.gyroWatcher.Stop();
        }

        /// <summary>
        /// Get the current rotation rate. This value is in radians per second.
        /// </summary>
        /// <returns>The MainViewModel object.</returns>
        void gyroscope_CurrentValueChanged(object sender, SensorReadingEventArgs<GyroscopeReading> e)
        {
            AddJOValue("currentRotationRateX", e.SensorReading.RotationRate.X);
            AddJOValue("currentRotationRateY", e.SensorReading.RotationRate.Y);
            AddJOValue("currentRotationRateZ", e.SensorReading.RotationRate.Z);
            AddJOValue("timestamp", DeviceTools.GetUnixTime());
        }
    }
}
