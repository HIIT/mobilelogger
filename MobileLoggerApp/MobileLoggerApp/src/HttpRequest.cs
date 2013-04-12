using System;
using System.IO;
using System.Net;
using System.Windows;

namespace MobileLoggerApp
{
    class HttpRequest
    {
        HttpRequestable source;

        public HttpRequest(string uri, HttpRequestable source)
        {
            this.source = source;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            request.BeginGetResponse(GetResponseCallback, request);
        }

        /// <summary>
        /// Asynchronous method that gets a request stream, required step for using GET with Google API
        /// </summary>
        /// <param name="asynchronousResult">Asynchronous result of the former http request object</param>
        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            request.BeginGetResponse(GetResponseCallback, request);
        }

        /// <summary>
        /// Gets the response to the query from Google, handles response back to main page
        /// </summary>
        /// <param name="ar">Asynchronous result of the former http request object</param>
        private void GetResponseCallback(IAsyncResult ar)
        {
            HttpWebRequest request = (HttpWebRequest)ar.AsyncState;
            String data;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
                var stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                data = reader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine("\nhererererer\n");

                System.Diagnostics.Debug.WriteLine(data.Length);

                System.Diagnostics.Debug.WriteLine("\nhererererer\n");

               

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        this.source.Callback(data);
                    });
            }
            catch (Exception exception)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    this.source.HandleRequestError(exception);
                });
            }
        }
    }
}
