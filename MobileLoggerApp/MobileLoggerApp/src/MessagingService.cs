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
                SendMessage(e);   
            }
        }

        private void SendMessage(Message e)
        {
            String responseSHA1 = e.SendMessage();
            if (responseSHA1 == e.GetHashCode())
            {
                //saving ok, remove entry from DB so it won't be sent again
                DBNull.removeEntry(e);
            }

        }

        private void SendMessage(String e)
        {

        }

        private void GetMessage()
        {
        }


    }
}
