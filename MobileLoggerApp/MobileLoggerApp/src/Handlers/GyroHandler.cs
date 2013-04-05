using Microsoft.Devices.Sensors;
using MobileLoggerScheduledAgent.Devicetools;
using System;

namespace MobileLoggerApp.Handlers
{
    public class GyroHandler : AbstractLogHandler
    {
        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, "/log/gyro");
        }

        public void StartGyroWatcher()
        {
            if (Microsoft.Devices.Environment.DeviceType != Microsoft.Devices.DeviceType.Emulator)
            {
                Gyroscope gyroWatcher = new Gyroscope();
                gyroWatcher.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                gyroWatcher.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<GyroscopeReading>>(gyroscope_CurrentValueChanged);
            }
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
