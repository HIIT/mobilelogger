using System;
using Microsoft.Phone.Controls;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows;
using System.Globalization;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MobileLoggerApp.src.model
{
    //a class that contains information and state of a http message
    public sealed class Message : HttpWebRequest
    {
        //empty constructor for message
        public Message()
        {
            Uri = "";
            Text = "";
            base.Method = "";
        }

        //Refaktoroi pois! tee julkinen navigointi-konteksti!!!
        public Message(string uri, string message, string method, PhoneApplicationPage src) 
        {
            Source = (MainPage)src;
            Uri = uri;
            Text = message;
            base.Method = method;
        }

        //constructor with parameters for the required values
        [Obsolete("Use Message(string uri, JObject message, string method")]
        public Message(string uri, string message, string method)
        {
            Uri = uri;
            Text = message;
            base.Method = method;
        }

        public Message(string uri, JObject message, string method)
        {
            // add device id + timestamp         
            Uri = uri;
            Text = JsonConvert.SerializeObject(message);
            base.Method = method;
        }

        public string Uri { get; set; }
        public string Text { get; set; }
        public string getMethod() 
        {
            return base.Method;
        }
        public MainPage Source { get; set; }

        public bool IsProperMessage() 
        {
            if(Uri=="")
            {
                return false;
            }

            if (base.Method == "")
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
