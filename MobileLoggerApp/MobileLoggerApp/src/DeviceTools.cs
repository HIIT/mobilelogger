using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileLoggerApp.src
{
    public class DeviceTools
    {
        /// <summary>
        /// Gets the Device Unique ID of the phone
        /// </summary>
        /// <returns>Device Unique ID as a string</returns>
        public static string GetDeviceId()
        {
            byte[] id = (byte[])Microsoft.Phone.Info.DeviceExtendedProperties.GetValue("DeviceUniqueId");
            return Convert.ToBase64String(id);
        }

        /// <summary>
        /// Turns a .NET DateTime timestamp into a unix timestamp for use at serverside
        /// </summary>
        /// <param name="time">The timestamp to be translated</param>
        /// <returns>unix timestamp</returns>
        public static double GetUnixTime(DateTime time)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = time - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// Turns a unix timestamp into a .NET DateTime for use at phoneside
        /// </summary>
        /// <param name="unixTimeStamp">The timestamp to be translated</param>
        /// <returns>.NET DateTime timestamp</returns>
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
