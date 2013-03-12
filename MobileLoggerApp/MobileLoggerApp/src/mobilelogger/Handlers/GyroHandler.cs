using Microsoft.Devices.Sensors;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class GyroHandler : AbstractLogHandler
    {
        Gyroscope gyroWatcher;
        JObject joGyro;

        public override void SaveSensorLog()
        {
            SaveLogToDB(joGyro, "/log/gyro");
        }

        public void StartGyroWatcher()
        {
            if (Microsoft.Devices.Environment.DeviceType != Microsoft.Devices.DeviceType.Emulator)
            {
                if (gyroWatcher == null)
                {
                    // Instantiate the Gyroscope.
                    gyroWatcher = new Gyroscope();
                    // Specify the desired time between updates. The sensor accepts
                    // intervals in multiples of 20 ms.
                    gyroWatcher.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                    gyroWatcher.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<GyroscopeReading>>(gyroscope_CurrentValueChanged);
                }
                gyroWatcher.Start();
            }
            if (joGyro == null)
            {
                joGyro = new JObject();
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
        }

        private void AddJOValue(String key, double value)
        {
            if (joGyro[key] == null)
            {
                joGyro.Add(key, (float)value);
            }
            else
            {
                joGyro[key].Replace((float)value);
            }
        }
    }
}
