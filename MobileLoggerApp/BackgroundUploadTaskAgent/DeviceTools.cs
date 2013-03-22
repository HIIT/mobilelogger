using System;
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
        public static long GetUnixTime(DateTime time)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = time - origin;
            return (long) Math.Floor(diff.TotalMilliseconds);
        }

        public static long GetUnixTime()
        {
            DateTime time = DateTime.UtcNow;
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = time - origin;
            return (long)Math.Floor(diff.TotalMilliseconds);
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
            catch (Exception e)
            {
                return origin.AddMilliseconds(unixTimeStamp);
            }
        }

        public static bool SensorLastSavedTimeSpan(DateTime lastSaved)
        {
            TimeSpan timeSpan = DateTime.UtcNow - lastSaved;
            if (timeSpan.TotalMilliseconds > 1000)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Calculates SHA1 checksum based on string source, source is assumed to be of json format
        /// </summary>
        /// <param name="src">json string</param>
        /// <returns>The string representation of the calculated SHA1 hash in uppercase</returns>
        public static string CalculateSHA1(String src)
        {
            using (System.Security.Cryptography.SHA1Managed sha1 = new System.Security.Cryptography.SHA1Managed())
            {
                String hash = BitConverter.ToString
                    (sha1.ComputeHash
                        (Encoding.UTF8.GetBytes
                            (src))).Replace("-", String.Empty);

                return hash;
            }
        }
    }
}
