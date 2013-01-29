using Microsoft.Phone.Controls;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Navigation;
using MobileLoggerApp;

namespace MobileLoggerApp.src
{
    class Connection
    {

        //Test method to send dummy data
        public static void PostTestCase(PhoneApplicationPage src)
        {
            string testiUri = "http://t-jonimake.users.cs.helsinki.fi/MobileLoggerServerDev/log";
            string testiViesti = "terve";
            string testiMetodi = "POST";
            MessageToUrl(testiUri, testiViesti, testiMetodi, src);
        }

        //sends a message to an url
        private static void MessageToUrl(string uri, string message, string method, PhoneApplicationPage src)
        {
            Message msg = new Message(uri, message, method, src);
            msg.SendMessage();
        }
    }
}
