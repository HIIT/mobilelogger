using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Windows;

namespace MobileLoggerApp.src
{
    class GoogleCustomSearch
    {
        private MainPage context;
        private string searchQuery;
        private int page;

        public delegate void SearchDataHandler(JObject searchData);
        public static event SearchDataHandler searchDataEvent;

        /// <summary>
        /// Constructor for the Google Search handles a Google search asynchronously
        /// </summary>
        /// <param name="source">The MainPage that called this constructor, is required for response logic later on</param>
        public GoogleCustomSearch(MainPage source)
        {
            context = source;
        }

        /// <summary>
        /// Synchronous public method that initiates the Google Search
        /// </summary>
        /// <param name="query">The search string that is queryed from Google</param>
        public void Search(string query, int page)
        {
            this.page = page;
            searchQuery = query;
            //string that contains required api key and information for google api
            string uri = String.Format("https://www.googleapis.com/customsearch/v1?key=AIzaSyDC_Y2CPa_zvLfgd09pLPoyd02hhvyaN8c&cx=011471749289680283085:rxjokcqp-ae&q={0}&start={1}", query, page);
            //Alternative search engine and an api-key, used for testing purposes.
            //string uri = String.Format("https://www.googleapis.com/customsearch/v1?key=AIzaSyCurZXbVyfaksuWlOaQVys5YwbewaBrtCs&cx=014771188109725738891:bcuskpsruhe&q={0}", query);

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
            string data;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
                var stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                data = reader.ReadToEnd();
                JObject searchData = JObject.Parse(data);
                JArray searchResults = (JArray)searchData["items"];

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    context.UpdateSearchResults(searchResults, page == 1);
                });

                searchDataEvent(searchData);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("{0}, {1} exception at ElGoog.GetResponseCallback", exception.Message, exception.StackTrace);
                data = null;
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (page > 1)
                    {
                        context.OpenBrowser(searchQuery);
                    }
                });
            }
        }
    }
}
