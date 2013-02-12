using System;
using Microsoft.Phone.Controls;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MobileLoggerApp.src
{
    /// <summary>
    /// Represents the state of a single HTTP PUT to the server
    /// </summary>
    public class Message
    {
        public string Uri { get; set; }
        public string Payload { get; set; }
        public string Response { get; set; }
        public bool done = false;
        public MainPage Source { get; set; }

        public HttpWebRequest request { get; internal set; }

        public static Message Create(string uri, string payload, string method)
        {
            Message message = new Message();
            message.request = (HttpWebRequest)WebRequest.Create(uri);
            message.request.Method = method;
            message.request.ContentType = "application/json";
            message.Payload = payload;
            return message;
        }        
    }  
}
