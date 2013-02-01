using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileLoggerApp.src;

namespace MobileLoggerTest.tests
{
    [TestClass]
    public class LogManagerTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            LogManager manager = new LogManager();
            Assert.IsInstanceOfType(manager, typeof(LogManager));
        }
    }
}
