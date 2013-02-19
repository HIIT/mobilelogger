﻿using System.Windows;
using Microsoft.Phone.Scheduler;
using System;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MobileLoggerScheduledAgent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        private static volatile bool _classInitialized;
        public static readonly string serverRoot = "http://t-jonimake.users.cs.helsinki.fi/MobileLoggerServerDev";

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
            System.Diagnostics.Debug.WriteLine("Unhandled exception"); 
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
            System.Diagnostics.Debug.WriteLine("OnInvoke");
            //TODO: Add code to perform your task in background
            SendMessages();
            
        }


        private void SendMessages()
        {
            System.Diagnostics.Debug.WriteLine("SendMessages");

            using (LogEventDataContext logDb = new LogEventDataContext(LogEventDataContext.ConnectionString))
            {
                if (!logDb.DatabaseExists())
                {
                    System.Diagnostics.Debug.WriteLine(this.GetType().Name + ": DB does not exist");
                    return;
                }

                logDb.addEvent("{\"phoneId\":string,\"text\":string,\"timestamp\":" + DeviceTools.GetUnixTime(DateTime.UtcNow) + ",\"lat\":12.3,\"lon\":12.3,\"alt\":12.3}", "/log/gps");
                System.Diagnostics.Debug.WriteLine(this.GetType().Name + ": GetLogEvents().size() " + logDb.GetLogEvents().Count);
                LogEvent e = new LogEvent();
                e.sensorEvent = "{\"phoneId\":string,\"text\":string,\"timestamp\":" + DeviceTools.GetUnixTime(DateTime.UtcNow) + ",\"lat\":12.3,\"lon\":12.3,\"alt\":12.3}";
                e.relativeUrl = "/log/gps";
                //foreach (LogEvent e in logDb.GetLogEvents())
               // {
                    SendMessage(e);
                //}
            }
            System.Diagnostics.Debug.WriteLine(this.GetType().Name + ".AsyncSendMessages event handler finished");
        }

        private void SendMessagesWorkComplete(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("finished send messages work");
            System.Diagnostics.Debug.WriteLine(sender.ToString() + " " + args.ToString());
        }


        private void SendMessage(LogEvent logevent)
        {

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] bytes = encoding.GetBytes(logevent.sensorEvent);

            Hammock.RestClient client = new Hammock.RestClient();
            client.Method = Hammock.Web.WebMethod.Put;
            client.Encoding = encoding;
            client.AddHeader("Content-type", "application/json");
            client.AddPostContent(bytes);
            client.Path = serverRoot + logevent.relativeUrl;
            client.BeginRequest(new Hammock.RestCallback(myCallback));

            /*

            WebClient web = new WebClient();
            web.UploadStringCompleted += web_UploadStringCompleted;
            web.UploadProgressChanged += web_UploadProgressChanged;
            web.WriteStreamClosed += web_WriteStreamClosed;

            Uri uri = new Uri(serverRoot + "/log");
            web.UploadStringAsync(uri, "PUT", logevent.sensorEvent);

            */
            //web.UploadStringAsync(uri, "PUT", logevent.sensorEvent);
        }

        private void myCallback(Hammock.RestRequest request, Hammock.RestResponse response, object userState)
        {
            System.Diagnostics.Debug.WriteLine("Status: " + response.StatusCode);
        }

        void web_WriteStreamClosed(object sender, WriteStreamClosedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("web_WriteStreamClosed");
        }

        void web_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("web_UploadProgressChanged");
            System.Diagnostics.Debug.WriteLine(e.ProgressPercentage);
        }

        private void web_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            WebClient client = (WebClient)sender;
            
            System.Diagnostics.Debug.WriteLine("web_UploadStringCompleted");
            System.Diagnostics.Debug.WriteLine("BaseAddress " + client.BaseAddress);
            System.Diagnostics.Debug.WriteLine("Credentials " + client.Credentials);
            System.Diagnostics.Debug.WriteLine("Encoding " + client.Encoding);
            NotifyComplete();    
        }
            /*

            // Create the web request object
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(serverRoot + e.relativeUrl);
            webRequest.Method = "PUT";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            // Start the request
            //webRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), webRequest);
            webRequest.BeginGetRequestStream(
                 result =>
                 {
                     System.Diagnostics.Debug.WriteLine("BeginGetReqeustStream");
                     try
                     {
                         System.Diagnostics.Debug.WriteLine("in try");
                         HttpWebRequest request1 = (HttpWebRequest)result.AsyncState;
                         Stream putStream = request1.EndGetRequestStream(result);

                         //tring info = string.Format("{0}{1}{2}{1}AppVersion: {3}{1}", exception.Message, Environment.NewLine, exception.StackTrace, exception.AppVersion);

                         string data = HttpUtility.UrlEncode(e.sensorEvent);
                         byte[] byteArray = Encoding.UTF8.GetBytes(data);
                         
                         System.Diagnostics.Debug.WriteLine("data length bytes: " + byteArray.Length);
                         putStream.Write(byteArray, 0, byteArray.Length);
                         putStream.Close();

                         request1.BeginGetResponse(
                             s =>
                             {
                                 System.Diagnostics.Debug.WriteLine("BeginGetResponse");
                                 try
                                 {
                                     System.Diagnostics.Debug.WriteLine("in try");
                                     HttpWebRequest request2 = (HttpWebRequest)s.AsyncState;
                                     HttpWebResponse response = (HttpWebResponse)request2.EndGetResponse(s);

                                     Stream streamResponse = response.GetResponseStream();
                                     StreamReader streamReader = new StreamReader(streamResponse);
                                     string responseStr = streamReader.ReadToEnd();
                                     System.Diagnostics.Debug.WriteLine("response: " + responseStr);
                                     //TODO VALIDATION

                                     streamResponse.Close();
                                     streamReader.Close();
                                     response.Close();
                                 }
                                 catch (Exception ex)
                                 {
                                     System.Diagnostics.Debug.WriteLine(ex.Message);
                                 }
                             },
                         request1);
                     }
                     catch (Exception ex)
                     {
                         System.Diagnostics.Debug.WriteLine(ex.Message);
                     }
                 },
             webRequest);
             */ 
        

       

        void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
            // End the stream request operation
            Stream postStream = webRequest.EndGetRequestStream(asynchronousResult);

            // Create the post data
            // Demo POST data 
            string postData = "data=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";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Add the post data to the web request
            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();

            // Start the web request
            webRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), webRequest);
        }

        void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                HttpWebResponse response;

                // End the get response operation
                response = (HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);
                Stream streamResponse = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(streamResponse);
                var Response = streamReader.ReadToEnd();
                streamResponse.Close();
                streamReader.Close();
                response.Close();

            }
            catch (WebException e)
            {
                // Error treatment
                // ...
            }
        }
    }
}