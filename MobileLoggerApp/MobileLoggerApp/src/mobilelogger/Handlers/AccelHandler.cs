using Microsoft.Devices.Sensors;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class AccelHandler : AbstractLogHandler
    {
        Accelerometer accelerometerWatcher;
        JObject joAccel;

        DateTime lastSaved;

        public override void SaveSensorLog()
        {
            if (DeviceTools.SensorLastSavedTimeSpan(lastSaved))
            {
                SaveLogToDB(joAccel, "/log/accel");
                lastSaved = DateTime.UtcNow;
            }
        }

        public void StartAccelWatcher()
        {
            if (accelerometerWatcher == null)
            {
                // Instantiate the Accelerometer.
                accelerometerWatcher = new Accelerometer();
                //accelerometerwatcher.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                accelerometerWatcher.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(accelerometer_CurrentValueChanged);
            }
            accelerometerWatcher.Start();

            if (joAccel == null)
            {
                joAccel = new JObject();
            }
        }

        void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            AddJOValue("accX", e.SensorReading.Acceleration.X);
            AddJOValue("accY", e.SensorReading.Acceleration.Y);
            AddJOValue("accZ", e.SensorReading.Acceleration.Z);
        }

        private void AddJOValue(String key, double value)
        {
            if (joAccel[key] == null)
            {
                joAccel.Add(key, (float)value);
            }
            else
            {
                joAccel[key].Replace((float)value);
            }
        }
    }
}
