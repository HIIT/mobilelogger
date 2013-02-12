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



            accelerometerwatcher.Start();

            if (joAccel == null)
            {
                joAccel = new JObject();
            }


        }
        void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {

            if (joAccel["X"] == null)
            {
                joAccel.Add("X", (float)e.SensorReading.Acceleration.X);
            }
            else
            {
                joAccel["X"].Replace((float)e.SensorReading.Acceleration.X);
            }

            if (joAccel["Y"] == null)
            {
                joAccel.Add("Y", (float)e.SensorReading.Acceleration.Y);
            }
            else
            {
                joAccel["Y"].Replace((float)e.SensorReading.Acceleration.Y);
            }

            if (joAccel["Z"] == null)
            {
                joAccel.Add("Z", (float)e.SensorReading.Acceleration.Z);
            }
            else
            {
                joAccel["Z"].Replace((float)e.SensorReading.Acceleration.Z);
            }
        }
    }
}




    


