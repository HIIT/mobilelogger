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
    public class Message : HttpWebRequest
    {
        public string Uri { get; set; }
        public string Payload { get; set; }
        public string Response { get; set; }
        public bool done = false;
        public override string Method { get; set; }
        public MainPage Source { get; set; }

        public new static Message Create(String uri)  
        {
            Message message = (Message)WebRequest.Create(uri);
            return message;
        }

        public static Message Create(string uri, string payload, string method)
        {
            Message message = (Message)WebRequest.Create(uri);
            message.Payload = payload;
            message.Method = method;
            return message;
        }

        //empty constructor for message
        [Obsolete("Use Create instead")]
        private Message()
        {
            Uri = "";
            Payload = "";
            Method = "";
        }

        //Refaktoroi pois! tee julkinen navigointi-konteksti!!!
        [Obsolete("Use Create instead")]
        private Message(string uri, string message, string method, PhoneApplicationPage src) 
        {
            Source = (MainPage)src;
            Uri = uri;
            Payload = message;
            Method = method;
        }

        //constructor with parameters for the required values
        [Obsolete("Use Create instead")]
        private Message(string uri, string message, string method)
        {
            Uri = uri;
            Payload = message;
            Method = method;
        }

        [Obsolete("Use Create instead")]
        private Message(string uri, JObject message, string method)
        {
            // add device id + timestamp

            byte[] id = (byte[])Microsoft.Phone.Info.DeviceExtendedProperties.GetValue("DeviceUniqueId");
            string phoneId = Convert.ToBase64String(id);
            message.Add("phoneId", phoneId);

            Uri = uri;
            Payload = JsonConvert.SerializeObject(message);
            Method = method;
        }



        //testaaminen hanakalaa, koska navigointi ja demoa varten oleva dispatcher
        [Obsolete("Use MessagingService instead")]
        public string SendMessage()
        {
            if (IsProperMessage())
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Uri);
                request.Method = Method;
                request.BeginGetRequestStream(GetRequestStreamCallback, request);
            }
            return "SHA1";
        }

        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            using (var outStream = request.EndGetRequestStream(asynchronousResult))
            {
                StreamWriter writer = new StreamWriter(outStream);
                writer.Write(Payload);
            }
            request.BeginGetResponse(GetResponseCallback, request);
        }

        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            Message request = (Message)asynchronousResult.AsyncState;
            string data;
            try
            {
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
                var stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                data = reader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(data+" at Message.GetResponseCallBack");
                //calls for the UI update! kinda ugly hardcoding
                Deployment.Current.Dispatcher.BeginInvoke(() => 
                {
                    var date = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
                    date = date.AddMilliseconds(Double.Parse(data));
                    //Source.navigateToPage(string.Format(PageLocations.responsePageUri + "?Val1={0}", date));
                });
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("{0}, {1} exception", exception.Message, exception.StackTrace);
                data = null;
            }
        }

        public void SetPayload(JObject json)
        {
            this.Payload = JsonConvert.SerializeObject(json);
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

            if (Payload == "")
            {
                return false;
            }

            return true;
        }
    }
}
