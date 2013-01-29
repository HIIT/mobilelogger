using Microsoft.Phone.Controls;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Navigation;
using MobileLoggerApp;

namespace MobileLoggerApp.src
{
    class Connection
    {

        public static void PostTestCase()
        {
            string testiUri = "http://t-jonimake.users.cs.helsinki.fi/MobileLoggerServerDev/log";
            string testiViesti = "terve";
            string testiMetodi = "POST";
            MessageToUrl(testiUri, testiViesti, testiMetodi);
        }

        private static void MessageToUrl(string uri, string message, string method)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = method;
            string data = null;

            request.BeginGetRequestStream(delegate(IAsyncResult req)
            {
                var outStream = request.EndGetRequestStream(req);
                using (StreamWriter writer = new StreamWriter(outStream))
                    writer.Write(message);

                request.BeginGetResponse(delegate(IAsyncResult result)
                {
                    try
                    {
                        HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

                        using (var stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                data = reader.ReadToEnd();
                                System.Diagnostics.Debug.WriteLine(data);
                                
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        System.Diagnostics.Debug.WriteLine("{0} exception", exception);
                        data = null;
                    }
                }, null);
            }, null);
        
        }
    }
}
