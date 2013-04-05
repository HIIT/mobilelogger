using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileLoggerScheduledAgent.Devicetools;
using System;

namespace MobileLoggerTest.tests
{
    [TestClass]
    public class DeviceToolsTest
    {
        [TestMethod]
        public void TestTimeConversion()
        {
            double unixtime = 1359720000;
            DateTime dateTime = new DateTime(2013, 2, 1, 12, 0, 0);
            Assert.AreEqual(unixtime, DeviceTools.GetUnixTime(dateTime));
            Assert.AreEqual(dateTime, DeviceTools.GetDateTime(unixtime));
        }

        [TestMethod]
        public void TestCalculateSHA1()
        {
            string src = "tamaonsha1hashi";
            string expectedsha1 = "f0e3fc8f9a3e3d27789482075293c7a6a3a24c06";

            string result = DeviceTools.CalculateSHA1(src);
            Assert.AreEqual(expectedsha1.ToUpper(), result);

        }
    }
}
