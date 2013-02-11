using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Cryptography;

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
