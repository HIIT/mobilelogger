using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Security;

namespace MobileLoggerApp.src
{
    public class MessagingService
    {
        public MessagingService() { }

        /// <summary>
        /// Tries to send all items in the phone DB to the server
        /// </summary>
        /// <returns></returns>
        public void SendMessages()
        {
            List<string> events = new List<string>{ "derp", "herp" };
            foreach (string e in events)
            {
                
            }
        }

        

        private void GetMessage()
        {
        }


    }
}
