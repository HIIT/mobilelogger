using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Devices.Sensors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class AccelHandler : AbstractLogHandler
    {
        Accelerometer accelerometerwatcher;
    
        JObject joAccel;

        public override void SaveSensorLog()
        {
            SaveLogToDB(joAccel, "/log/accel");
        }

        public void startAccelWatcher()
        {
            if (accelerometerwatcher == null)
            {
                // Instantiate the Accelerometer.
                accelerometerwatcher = new Accelerometer();
                //accelerometer.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                accelerometerwatcher.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(accelerometer_CurrentValueChanged);
            }

        }
        void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            joAccel.Add("X", (float)e.SensorReading.Acceleration.X);
            joAccel.Add("Y", (float)e.SensorReading.Acceleration.Y);
            joAccel.Add("Z", (float)e.SensorReading.Acceleration.Z);
        }
    }
}




    


