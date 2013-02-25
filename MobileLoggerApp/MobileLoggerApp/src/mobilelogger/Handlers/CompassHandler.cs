using Microsoft.Devices.Sensors;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class CompassHandler : AbstractLogHandler
    {
        Compass compass;
        JObject joCompass;

        DateTime lastSaved;

        public override void SaveSensorLog()
        {
            if (DeviceTools.SensorLastSavedTimeSpan(lastSaved))
            {
                SaveLogToDB(joCompass, "/log/compass");
                lastSaved = DateTime.UtcNow;
            }
        }
        public void startCompassWatcher()
        {
#if EMULATOR
#endif
            if (Microsoft.Devices.Environment.DeviceType != Microsoft.Devices.DeviceType.Emulator)
            {
                if (compass == null)
                {
                    // Instantiate the Compass.
                    compass = new Compass();
                    //compass.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                    compass.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<CompassReading>>(compass_CurrentValueChanged);
                    //compass.Calibrate += new EventHandler<CalibrationEventArgs>(compass_Calibrate);
                }
                compass.Start();
            }
            if (joCompass == null)
            {
                joCompass = new JObject();
            }
        }

        void compass_CurrentValueChanged(object sender, SensorReadingEventArgs<CompassReading> e)
        {
            if (joCompass["trueHeading"] == null)
            {
                joCompass.Add("trueHeading", (float)e.SensorReading.TrueHeading);
            }
            else
            {
                joCompass["trueHeading"].Replace((float)e.SensorReading.TrueHeading);
            }

            if (joCompass["magneticHeading"] == null)
            {
                joCompass.Add("magneticHeading", (float)e.SensorReading.MagneticHeading);
            }
            else
            {
                joCompass["magneticHeading"].Replace((float)e.SensorReading.MagneticHeading);
            }

            if (joCompass["headingAccuracy"] == null)
            {
                joCompass.Add("headingAccuracy", (float)Math.Abs(e.SensorReading.HeadingAccuracy));
            }
            else
            {
                joCompass["headingAccuracy"].Replace((float)Math.Abs(e.SensorReading.HeadingAccuracy));
            }
            if (joCompass["rawMagneticReadingX"] == null)
            {
                joCompass.Add("rawMagneticReadingX", (float)e.SensorReading.MagnetometerReading.X);
            }
            else
            {
                joCompass["rawMagneticReadingX"].Replace((float)e.SensorReading.MagnetometerReading.X);
            }

            if (joCompass["rawMagneticReadingY"] == null)
            {
                joCompass.Add("rawMagneticReadingY", (float)e.SensorReading.MagnetometerReading.Y);
            }
            else
            {
                joCompass["rawMagneticReadingY"].Replace((float)e.SensorReading.MagnetometerReading.Y);
            }
            if (joCompass["rawMagneticReadingZ"] == null)
            {
                joCompass.Add("rawMagneticReadingZ", (float)e.SensorReading.MagnetometerReading.Z);
            }
            else
            {
                joCompass["rawMagneticReadingZ"].Replace((float)e.SensorReading.MagnetometerReading.Z);
            }
        }
    }
}
