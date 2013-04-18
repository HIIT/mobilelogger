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
        }

        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, "/log/gyro");
        }

        public override void StartWatcher()
        {
            if (Microsoft.Devices.Environment.DeviceType != Microsoft.Devices.DeviceType.Emulator)
            {
                if (Gyroscope.IsSupported)
                {
                    this.gyroWatcher.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                    this.gyroWatcher.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<GyroscopeReading>>(gyroscope_CurrentValueChanged);
                    this.gyroWatcher.Start();
                    this.IsEnabled = true;
                }
            }
        }

        public override void StopWatcher()
        {
            this.gyroWatcher.Stop();
            this.IsEnabled = false;
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
