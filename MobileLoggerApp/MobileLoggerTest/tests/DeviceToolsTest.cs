using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileLoggerApp.src;

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
    }
}
