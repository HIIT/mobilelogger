using System;
using System.IO;
using System.Net;

namespace MobileLoggerApp.src
{
    class Connection
    {
        public static void PostTestCase()
        {
            string testiUri = "http://t-jonimake.users.cs.helsinki.fi/MobileLoggerServerDev/postFromPhone";
            string testiViesti = "terve";
            PostToUrl(testiUri, testiViesti);
            
        }

        private static void PostToUrl(string uri, string message)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
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
                                var data = reader.ReadToEnd();
                                System.Diagnostics.Debug.WriteLine(data);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        System.Diagnostics.Debug.WriteLine("{0} exception", exception);
                    }
                }, null);
            }, null);
        }
    }
}
