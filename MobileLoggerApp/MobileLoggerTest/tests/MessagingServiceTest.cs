using System;
using MobileLoggerApp.src;
using System.Threading;
using System.Windows;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MobileLoggerTest.tests
{
    [TestClass]
    public class MessagingServiceTest
    {
        
        [TestCase]
        public void TestSendMessage()
        {

            
            MessagingService service = new MessagingService();


            
            Console.WriteLine("before sendmessages");
           // service.SendMessages();
            Console.WriteLine("after sendmessages");
         
        }
        
    }
}
