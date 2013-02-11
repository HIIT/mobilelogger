using System;
using MobileLoggerApp.src;
using System.Threading;
using System.Windows;
using NUnit.Framework;
namespace MobileLoggerTest.tests
{
    [TestFixture]
    public class MessagingServiceTest
    {
        [Test]
        public void TestSendMessage()
        {

            
            MessagingService service = new MessagingService();
            Console.WriteLine("before sendmessages");
            service.SendMessages();
            Console.WriteLine("after sendmessages");
         
        }
    }
}
