using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileLoggerApp.src.mobilelogger.model;
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
            using (LogEventDataContext logDBContext = new LogEventDataContext(MainPage.ConnectionString))
            {
                if (!logDBContext.DatabaseExists()) return;
                SHA1Managed sha = new SHA1Managed();
                foreach(LogEvent e in logDBContext.GetLogEvents()) 
                {
                    SendMessage(e);
                    
                }
            }
        }


        //get checksum
        //send data to server
        //wait for answer checksum
        //compare checksums
        //if equal, sending was ok, remove from DB
        //else keep in DB and continue
        private void SendMessage(LogEvent e)
        {
            String checksum = CalculateCheckSHA1(e.sensorEvent);
            Message message = Message.Create("");
        }

        private string CalculateCheckSHA1(String src)
        {
            SHA1Managed sha = new SHA1Managed();
            byte[] digest = sha.ComputeHash(Convert.FromBase64String(src));
            return Convert.ToBase64String(digest);
        }


        private void GetMessage()
        {
        }


    }
}
