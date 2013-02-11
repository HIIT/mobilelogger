using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MobileLoggerApp.src.mobilelogger.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Security;
using System.Windows.Threading;


namespace MobileLoggerApp.src
{
    public class MessagingService// : DispatcherTimer
    {

        

        public BackgroundWorker bgworker;

        public MessagingService()
        {
            InitializeBackgroundWorker();
            //this.Interval = new TimeSpan(0,0,1);
        }

        private void InitializeBackgroundWorker()
        {
            bgworker = new BackgroundWorker();
            bgworker.DoWork += new DoWorkEventHandler(AsyncSendMessages);
            bgworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SendMessagesWorkComplete);
        }

        /// <summary>
        /// Tries to send all items in the phone DB to the server
        /// </summary>
        /// <returns></returns>
        public void SendMessages()
        {
            Console.WriteLine("SendMessages");
            System.Diagnostics.Debug.WriteLine("SendMessages");
            this.bgworker.RunWorkerAsync();
        
        }

        /// <summary>
        /// Fired when bgworker.RunWorkerAsync() is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void AsyncSendMessages(object sender, EventArgs args) 
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Console.WriteLine("event working");
            System.Diagnostics.Debug.WriteLine("event working");
            Console.WriteLine(sender.ToString() + " " + args.ToString());
            using (LogEventDataContext logDBContext = new LogEventDataContext(MainPage.ConnectionString))
            {
                
                if (!logDBContext.DatabaseExists()) return;
                SHA1Managed sha = new SHA1Managed();
                foreach (LogEvent e in logDBContext.GetLogEvents())
                {
                    SendMessage(e);
                }
            }
        }

        /// <summary>
        /// Fired when bgworker.RunWorkerAsync() finishes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SendMessagesWorkComplete(object sender, EventArgs args) 
        {
            Console.WriteLine("event work complete");
            Console.WriteLine(sender.ToString() + " " + args.ToString());
            System.Diagnostics.Debug.WriteLine("finished send messages work");
            System.Diagnostics.Debug.WriteLine(sender.ToString() + " " + args.ToString());
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
            Message message = Message.Create("uri", "payload", "PUT");
        }

        private string CalculateCheckSHA1(String src)
        {
            SHA1Managed sha = new SHA1Managed();
            byte[] digest = sha.ComputeHash(Convert.FromBase64String(src));
            System.Diagnostics.Debug.WriteLine("SHA1 Digest for " + src + ": " + Convert.ToBase64String(digest));
            return Convert.ToBase64String(digest);
        }
    }
}
