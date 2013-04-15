using Microsoft.Phone.Scheduler;
using MobileLoggerScheduledAgent.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace MobileLoggerScheduledAgent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        private static volatile bool _classInitialized;
        public static readonly string serverRoot = "http://t-jonimake.users.cs.helsinki.fi/MobileLoggerServer";
        private static readonly int TIMEOUT_MS = 10000;
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        public ScheduledAgent()
        {
            System.Diagnostics.Debug.WriteLine("Initializing background task agent");
            if (!_classInitialized)
            {
                _classInitialized = true;
                // Subscribe to the managed exception handler
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    Application.Current.UnhandledException += ScheduledAgent_UnhandledException;
                });
            }
        }

        /// Code to execute on Unhandled Exceptions
        private void ScheduledAgent_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(this.GetType().Name + " Unhandled exception"); 
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                //System.Diagnostics.Debugger.Break();
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
        }


        private void SendMessages()
        {
            System.Diagnostics.Debug.WriteLine(this.GetType().Name +  ".SendMessages");

            using (LogEventDataContext logDb = new LogEventDataContext(LogEventDataContext.ConnectionString))
            {
                if (!logDb.DatabaseExists())
                {
                    System.Diagnostics.Debug.WriteLine(this.GetType().Name + ": DB does not exist");
                    return;
                }
                List<LogEvent> events = logDb.GetLogEvents();
                System.Diagnostics.Debug.WriteLine("Sending " + events.Count + " events");
                foreach (LogEvent e in events)
                {
                    SendMessage(e);
                }
            }
            System.Diagnostics.Debug.WriteLine(this.GetType().Name + ".AsyncSendMessages event handler finished");
        }

        private void SendMessage(LogEvent logevent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverRoot + logevent.relativeUrl);
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.BeginGetRequestStream(asynchronousResult =>
            {
                SendData(logevent, asynchronousResult);
            }, request);

        }

        private void SendData(LogEvent logevent, IAsyncResult asynchronousResult)
        {

            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            // End the operation
            Stream putStream = request.EndGetRequestStream(asynchronousResult);
            
            //Console.WriteLine("Please enter the input data to be posted:");
            string putData = logevent.sensorEvent;
            // Convert the string into a byte array. 
            byte[] byteArray = Encoding.UTF8.GetBytes(putData);
            //putStream.WriteTimeout = TIMEOUT_MS;
            // Write to the request stream.
            putStream.Write(byteArray, 0, putData.Length);
            
            putStream.Close();
            // Start the asynchronous operation to get the response
            request.BeginGetResponse(responseAsynchronousResult =>
            {
                GetResponse(responseAsynchronousResult, logevent);

            }, request);
            
        }

        private void GetResponse(IAsyncResult asynchronousResult, LogEvent logevent)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

                // End the operation
                HttpWebResponse finalresponse = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
                Stream streamResponse = finalresponse.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse);
                string responseString = streamRead.ReadToEnd();

                System.Diagnostics.Debug.WriteLine(responseString);

                if (responseString.Equals("true"))
                    deleteFromDB(logevent);
                else
                {
                    System.Diagnostics.Debug.WriteLine(this.GetType().Name + " Send failure");
                    System.Diagnostics.Debug.WriteLine(this.GetType().Name + " response: " + responseString);
                    System.Diagnostics.Debug.WriteLine(this.GetType().Name + " logevent: " + logevent.sensorEvent);
                    System.Diagnostics.Debug.WriteLine(this.GetType().Name + " url: " + logevent.relativeUrl);
                }
                // Close the stream object
               
                streamRead.Close();
                //streamResponse.Close();

                // Release the HttpWebResponse
                finalresponse.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(this.GetType().Name + " Exception");
                System.Diagnostics.Debug.WriteLine(this.GetType().Name + " logevent: " + logevent.sensorEvent);
                System.Diagnostics.Debug.WriteLine(this.GetType().Name + " url: " + logevent.relativeUrl);
                System.Diagnostics.Debug.WriteLine(e.GetBaseException());
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }

        private void deleteFromDB(LogEvent logevent)
        {
            using (LogEventDataContext logDb = new LogEventDataContext(LogEventDataContext.ConnectionString))
            {
                if (!logDb.DatabaseExists())
                {
                    System.Diagnostics.Debug.WriteLine(this.GetType().Name + ": DB does not exist");
                    return;
                }
                logDb.DeleteLogEvent(logevent.EventId);
            }
        }       
        
    }
}