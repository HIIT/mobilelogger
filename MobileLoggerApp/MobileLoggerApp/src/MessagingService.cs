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
using System.Net;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Microsoft.Phone.Scheduler;


namespace MobileLoggerApp.src
{
    public class MessagingService : ScheduledTaskAgent
    {
        // private DispatcherTimer timer;
        public BackgroundWorker bgworker;
        private bool _classInitialized = false;
        private string taskName;

        public MessagingService(string name)
        {
            this.taskName = name;
            if (!_classInitialized)
            {
                _classInitialized = true;
                // Subscribe to the managed exception handler
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    Application.Current.UnhandledException += ScheduledAgent_UnhandledException;
                });
            }

            InitializeBackgroundWorker();
        }

        /// Code to execute on Unhandled Exceptions
        private void ScheduledAgent_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            SendMessages();

            //NotifyComplete();
        }

        private void SendMessages()
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
            System.Diagnostics.Debug.WriteLine(this.GetType().Name + ".AsyncSendMessages event handler");
            BackgroundWorker worker = sender as BackgroundWorker;

            using (LogEventDataContext logDBContext = new LogEventDataContext(MainPage.ConnectionString))
            {
                if (!logDBContext.DatabaseExists())
                {
                    System.Diagnostics.Debug.WriteLine(this.GetType().Name + ": DB does not exist");
                    return;
                }
#if DEBUG
                System.Diagnostics.Debug.WriteLine(this.GetType().Name + ": GetLogEvents().size() " + logDBContext.GetLogEvents().Count);
                if (logDBContext.GetLogEvents().Count() == 0)
                {
                    logDBContext.addEvent("{derp:herp}", "/log/gps");
                }
#endif
                foreach (LogEvent e in logDBContext.GetLogEvents())
                {
                    SendMessage(e);
                }
            }
            System.Diagnostics.Debug.WriteLine(this.GetType().Name + ".AsyncSendMessages event handler finished");
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
        private async void SendMessage(LogEvent e)
        {
            //create request message
            Message message = Message.Create(ServerLocations.serverRoot + e.relativeUrl, e.sensorEvent, "PUT");

            System.Diagnostics.Debug.WriteLine("request uri " + message.request.RequestUri);

            using (Stream x = await message.request.GetRequestStreamAsync())
            {
                System.Diagnostics.Debug.WriteLine("writing yay");
                System.Diagnostics.Debug.WriteLine(message.Payload);
                StreamWriter writer = new StreamWriter(x);
                writer.Write(message.Payload);
                writer.Flush();
                writer.Close();
            }

            System.Diagnostics.Debug.WriteLine("written yay");
            try
            {
                using (WebResponse x = await message.request.GetResponseAsync())
                {
                    System.Diagnostics.Debug.WriteLine("got webresponse");
                    System.Diagnostics.Debug.WriteLine(x.GetResponseStream().Length);
                    x.Close();
                }
            }
            catch (Exception exp)
            {
                System.Diagnostics.Debug.WriteLine(exp.ToString());
            }
            /*
            message.request.BeginGetRequestStream(result =>
            {
                System.Diagnostics.Debug.WriteLine(this.GetType().Name + ": BeginGetRequestStream result " + result.ToString());
                //write to stream when requeststream is got
                WriteMessageToStream(result, message, e);
            }, null);
             */ 
        }

        private void WriteMessageToStream(IAsyncResult asynchronousResult, Message message, LogEvent e)
        {
            if (asynchronousResult.IsCompleted)
            {
                System.Diagnostics.Debug.WriteLine("is completed");
                System.Diagnostics.Debug.WriteLine(asynchronousResult.GetType());
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("is not completed");
            }

            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            
            System.Diagnostics.Debug.WriteLine(this.GetType().Name + ": " + request.ToString());
            using (var outStream = request.EndGetRequestStream(asynchronousResult))
            {
                StreamWriter writer = new StreamWriter(outStream);
                writer.Write(message.Payload);
                writer.Flush();
                writer.Close();
            }
            /*
            request.BeginGetResponse(result => 
            {
                System.Diagnostics.Debug.WriteLine(this.GetType().Name + ".WriteMessageToStream");
                //after getting response validate the response
                FinishWebRequest(result, message, e);
            }, null);
             */ 
        }
        
        //validates the response based on the checksums
        private void FinishWebRequest(IAsyncResult result, Message message, LogEvent e)
        {
            System.Diagnostics.Debug.WriteLine(this.GetType().Name + ".FinishWebRequest");
            String checksum = DeviceTools.CalculateSHA1(e.sensorEvent);

            using (HttpWebResponse response = (HttpWebResponse)message.request.EndGetResponse(result))
            {
                System.IO.Stream receiveStream = response.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, encode);
                
                Char[] read = new Char[256];
                // Reads 256 characters at a time.     
                int count = readStream.Read(read, 0, 256);
                System.Diagnostics.Debug.WriteLine("HTML...\r\n");
                while (count > 0)
                {
                    // Dumps the 256 characters on a string and displays the string to the console.
                    String str = new String(read, 0, count);
                    System.Diagnostics.Debug.WriteLine(str);
                    count = readStream.Read(read, 0, 256);
                }
                System.Diagnostics.Debug.WriteLine("");
                // Releases the resources of the response.
                response.Close();
                // Releases the resources of the Stream.
                readStream.Close();
                //validate response checksum
            }
        }
    }
}
