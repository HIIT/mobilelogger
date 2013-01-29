﻿using System;
using Microsoft.Phone.Controls;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;

namespace MobileLoggerApp.src
{
    //a class that contains information of a http message
    class Message
    {
        //empty constructor for messafe
        public Message()
        {
            Uri = "";
            Text = "";
            Method = "";
        }

        public Message(string uri, string message, string method, PhoneApplicationPage src) 
        {
            Source = (MainPage)src;
            Uri = uri;
            Text = message;
            Method = method;
        }

        //constructor with parameters for the required values
        public Message(string uri, string message, string method)
        {
            Uri = uri;
            Text = message;
            Method = method;
        }

        public string Uri { get; set; }
        public string Text { get; set; }
        public string Method { get; set; }
        public MainPage Source { get; set; }

        public void SendMessage()
        {
            if (IsProperMessage())
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Uri);
                request.Method = Method;
                request.BeginGetRequestStream(GetRequestStreamCallback, request);
            }
        }

        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            using (var outStream = request.EndGetRequestStream(asynchronousResult))
            {
                StreamWriter writer = new StreamWriter(outStream);
                writer.Write(Text);
            }
            request.BeginGetResponse(GetResponseCallback, request);
        }

        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            string data;
            try
            {
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
                var stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                data = reader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(data);
                //calls for the UI update!
                Deployment.Current.Dispatcher.BeginInvoke(() => 
                {
                    Source.navigateToPage(string.Format(PageLocations.responsePageUri + "?Val1={0}", data));
                });
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("{0}, {1} exception", exception.Message, exception.StackTrace);
                data = null;
            }
        }


        public bool IsProperMessage() 
        {
            if(Uri=="")
            {
                return false;
            }

            if (Method == "")
            {
                return false;
            }

            if (Text == "")
            {
                return false;
            }

            return true;
        }
    }
}
