using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Devices.Sensors;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class GyroHandler : AbstractLogHandler
    {
        Gyroscope gyroscope;
        JObject joGyro;

        Vector3 currentRotationRate = Vector3.Zero;
        Vector3 cumulativeRotation = Vector3.Zero;
        DateTimeOffset lastUpdateTime = DateTimeOffset.MinValue;
        bool isDataValid;

        public override void SaveSensorLog()
        {
            SaveLogToDB(joGyro, "/log/gyro");
        }
        public void startGyroWatcher()
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

            if (joGyro == null)
            {
                joGyro = new JObject();
            }
        }
        void gyroscope_CurrentValueChanged(object sender, SensorReadingEventArgs<GyroscopeReading> e)
        {
            //if (joGyro["lastUpdateTimeMilli"] == null)
            //{
            //    joGyro.Add("lastUpdateTimeMilli", (float)e.SensorReading.Timestamp.LocalDateTime.Millisecond);
            //}
            //else
            //{
            //    joGyro["lastUpdateTimeMilli"].Replace((float)e.SensorReading.Timestamp.LocalDateTime.Millisecond);
            //}
            //if (joGyro["lastUpdateTimeSecond"] == null)
            //{
            //    joGyro.Add("lastUpdateTimeSecond", (float)e.SensorReading.Timestamp.Second);
            //}
            //else
            //{
            //    joGyro["lastUpdateTimeSecond"].Replace((float)e.SensorReading.Timestamp.Second);
            //}

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
            } if (joGyro["currentRotationRateZ"] == null)
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