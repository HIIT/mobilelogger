﻿using System;
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
using System.Net;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;


namespace MobileLoggerApp.src
{
    public class MessagingService
    {
       // private DispatcherTimer timer;
         public BackgroundWorker bgworker;

        public MessagingService()
        {
            //timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(60);
            //timer.Tick += new EventHandler(timer_Tick);

            //timer.Start();
            InitializeBackgroundWorker();
            //this.Interval = new TimeSpan(0,0,1);
        }

        public void SendMessages()
        {
            System.Diagnostics.Debug.WriteLine("SendMessages");
            bgworker.RunWorkerAsync();
        }
        
        private void InitializeBackgroundWorker()
        {
            bgworker = new BackgroundWorker();
            bgworker.DoWork += new DoWorkEventHandler(AsyncSendMessages);
            bgworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SendMessagesWorkComplete);
        }

        private void AsyncSendMessages(object sender, DoWorkEventArgs args)
        {
            
            BackgroundWorker worker = sender as BackgroundWorker;

            using (LogEventDataContext logDBContext = new LogEventDataContext(MainPage.ConnectionString))
            {
                if (!logDBContext.DatabaseExists()) return;
                foreach (LogEvent e in logDBContext.GetLogEvents())
                {
                    SendMessage(e);
                }
            }
             
        }

        private void SendMessagesWorkComplete(object sender, EventArgs args)
        {
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
            String checksum = DeviceTools.CalculateSHA1(e.sensorEvent);

            //create request message
            Message message = Message.Create(e.relativeUrl, e.sensorEvent, "PUT");
            message.BeginGetRequestStream(result =>
            {
                FinishWebRequest(result, message, checksum);
            }, null); // Don't need the state here any more
        }

        //validates the response based on the checksums
        private void FinishWebRequest(IAsyncResult result, Message request, String checksum)
        {
            using (HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result))
            {
                System.IO.Stream receiveStream = response.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, encode);
                
                Char[] read = new Char[256];
                // Reads 256 characters at a time.     
                int count = readStream.Read(read, 0, 256);
                Console.WriteLine("HTML...\r\n");
                while (count > 0)
                {
                    // Dumps the 256 characters on a string and displays the string to the console.
                    String str = new String(read, 0, count);
                    Console.Write(str);
                    count = readStream.Read(read, 0, 256);
                }
                Console.WriteLine("");


                // Releases the resources of the response.
                response.Close();
                // Releases the resources of the Stream.
                readStream.Close();

                //validate response checksum

            }
        }
    }
}
