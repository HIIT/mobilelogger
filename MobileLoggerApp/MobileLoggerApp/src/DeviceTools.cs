using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileLoggerApp.src
{
    public class DeviceTools
    {
        public static string GetDeviceId()
        {
            byte[] id = (byte[])Microsoft.Phone.Info.DeviceExtendedProperties.GetValue("DeviceUniqueId");
            return Convert.ToBase64String(id);
        }

        public static double GetUnixTime(DateTime time)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = time - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public static DateTime GetDateTime(double unixTimeStamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            try
            {
                return origin.AddSeconds(unixTimeStamp);
            }
            catch(Exception e)
            {
                return origin.AddMilliseconds(unixTimeStamp);
            }
        }
    }
}
