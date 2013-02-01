using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileLoggerApp.src;
using MobileLoggerApp;

namespace MobileLoggerTest.tests
{
    [TestClass]
    public class MessageTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Message msg = new Message();
            Assert.IsInstanceOfType(msg, typeof(Message));
            Assert.AreEqual(msg.Method, "");
            Assert.AreEqual(msg.Text, "");
            Assert.AreEqual(msg.Uri, "");
            Assert.IsFalse(msg.IsProperMessage());
            msg.Uri="testiUri";
            Assert.IsFalse(msg.IsProperMessage());
            msg.Method = "testiMetodi";
            Assert.IsFalse(msg.IsProperMessage());

            msg = new Message("testUri", "testMessage", "testMethod");
            Assert.AreEqual(msg.Method, "testMethod");
            Assert.AreEqual(msg.Text, "testMessage");
            Assert.AreEqual(msg.Uri, "testUri");
            Assert.IsTrue(msg.IsProperMessage());

        }
    }
}
