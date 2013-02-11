using System;
using MobileLoggerApp.src;
using System.Threading;
using System.Windows;
using System.Timers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.Resources;

namespace MobileLoggerTest.tests
{
    [TestClass]
    public class MessagingServiceTest
    {
        
        [TestMethod]
        public void TestSendMessage()
        {

            MessagingService service = new MessagingService();
            System.Diagnostics.Debug.WriteLine("before sendmessages");

            service.SendMessages();
            System.Diagnostics.Debug.WriteLine("after sendmessages");
            
        }

        
        
    }
}
