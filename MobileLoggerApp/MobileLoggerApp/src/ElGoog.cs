using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Phone.Controls;

namespace MobileLoggerApp.src
{
    class ElGoog
    {
        private PhoneApplicationPage context;
        public ElGoog(PhoneApplicationPage source)
        {
            context = source;
        }

        public void Search(string query)
        {
            //string that contains required api key and information for google api
            string uri = String.Format("https://www.googleapis.com/customsearch/v1?key=AIzaSyDC_Y2CPa_zvLfgd09pLPoyd02hhvyaN8c&cx=011471749289680283085:rxjokcqp-ae&q={0}", query);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            //request.ContentType = "application/json; charset=utf-8";
            request.BeginGetResponse(GetResponseCallback, request);        
        }

        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            /*using (var outStream = request.EndGetRequestStream(asynchronousResult))
            {
                StreamWriter writer = new StreamWriter(outStream);
                writer.Write(Text);
            }*/
            request.BeginGetResponse(GetResponseCallback, request);
        }

        private void GetResponseCallback(IAsyncResult ar)
        {
            HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
            string data;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
                var stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                data = reader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(data.Length);
                JObject JSON = new JObject(data);

                //calls for the UI update! kinda ugly hardcoding
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var date = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
                    date = date.AddMilliseconds(Double.Parse(data));
                    context.Update(JSON);
                });
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("{0}, {1} exception", exception.Message, exception.StackTrace);
                data = null;
            }
        }
    }

    
}
