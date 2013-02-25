using Microsoft.Devices.Sensors;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class GyroHandler : AbstractLogHandler
    {
        Gyroscope gyroscope;
        JObject joGyro;

        DateTime lastSaved;

        public override void SaveSensorLog()
        {
            if (DeviceTools.SensorLastSavedTimeSpan(lastSaved))
            {
                SaveLogToDB(joGyro, "/log/gyro");
                lastSaved = DateTime.UtcNow;
            }
        }

        public void startGyroWatcher()
        {
            if (Microsoft.Devices.Environment.DeviceType != Microsoft.Devices.DeviceType.Emulator)
            {
                if (gyroscope == null)
                {
                    // Instantiate the Gyroscope.
                    gyroscope = new Gyroscope();
                    // Specify the desired time between updates. The sensor accepts
                    // intervals in multiples of 20 ms.
                    //gyroscope.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                    gyroscope.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<GyroscopeReading>>(gyroscope_CurrentValueChanged);
                }
                gyroscope.Start();
            }
            if (joGyro == null)
            {
                joGyro = new JObject();
            }
        }

        void gyroscope_CurrentValueChanged(object sender, SensorReadingEventArgs<GyroscopeReading> e)
        {
            // Get the current rotation rate. This value is in 
            // radians per second.
            if (joGyro["currentRotationRateX"] == null)
            {
                joGyro.Add("currentRotationRateX", (float)e.SensorReading.RotationRate.X);
            }
            else
            {
                joGyro["currentRotationRateX"].Replace((float)e.SensorReading.RotationRate.X);
            }
            if (joGyro["currentRotationRateY"] == null)
            {
                joGyro.Add("currentRotationRateY", (float)e.SensorReading.RotationRate.Y);
            }
            else
            {
                joGyro["currentRotationRateY"].Replace((float)e.SensorReading.RotationRate.Y);
            }
            if (joGyro["currentRotationRateZ"] == null)
            {
                joGyro.Add("currentRotationRateZ", (float)e.SensorReading.RotationRate.Z);
            }
            else
            {
                joGyro["currentRotationRateZ"].Replace((float)e.SensorReading.RotationRate.Z);
            }
        }
    }
}
