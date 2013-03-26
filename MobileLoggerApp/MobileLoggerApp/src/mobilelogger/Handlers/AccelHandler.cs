using Microsoft.Devices.Sensors;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class AccelHandler : AbstractLogHandler
    {
        Accelerometer accelerometerWatcher;

        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, "/log/accel");
        }

        public void StartAccelWatcher()
        {
            if (accelerometerWatcher == null)
            {
                accelerometerWatcher = new Accelerometer();
                accelerometerWatcher.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                accelerometerWatcher.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(accelerometer_CurrentValueChanged);
            }
            accelerometerWatcher.Start();

            if (this.data == null)
            {
                this.data = new JObject();
            }
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
