using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using MobileLoggerScheduledAgent.Devicetools;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;

namespace MobileLoggerApp
{
    class GoogleCustomSearch : HttpRequestable
    {
        private string searchQuery;
        private int searchPageNumber;

        public delegate void SearchDataHandler(JObject searchData);
        public static event SearchDataHandler searchDataEvent;

        /// <summary>
        /// Constructor for the Google Search handles a Google search asynchronously
        /// </summary>
        /// <param name="source">The MainPage that called this constructor, is required for response logic later on</param>
        public GoogleCustomSearch()
        {
        }

        /// <summary>
        /// Synchronous public method that initiates the Google Search
        /// </summary>
        /// <param name="query">The search string that is queryed from Google</param>
        public void Search(string query, int searchPageNumber)
        {
            SystemTray.ProgressIndicator.IsVisible = true;
            this.searchPageNumber = searchPageNumber;
            searchQuery = query;
            //string that contains required api key and information for google api
            string uri = String.Format("https://www.googleapis.com/customsearch/v1?key={2}&cx=011471749289680283085:rxjokcqp-ae&q={0}&start={1}", query, searchPageNumber, DeviceTools.googleApiKey);

            //Alternative search engine and an api-key, used for testing purposes.
            //string uri = String.Format("https://www.googleapis.com/customsearch/v1?key=AIzaSyCurZXbVyfaksuWlOaQVys5YwbewaBrtCs&cx=014771188109725738891:bcuskpsruhe&q={0}", query);

            HttpRequest request = new HttpRequest(uri, this);
        }

        public void Callback(string data)
        {
            JObject searchData = JObject.Parse(data);
            JArray searchResults = (JArray)searchData["items"];

            App.ViewModel.UpdateSearchResults(searchResults, searchPageNumber == 1);
            searchDataEvent(searchData);
            SystemTray.ProgressIndicator.IsVisible = false;
        }

        public void HandleRequestError(Exception exception)
        {
            System.Diagnostics.Debug.WriteLine("{0}, {1} exception at GoogleCustomSearch.GetResponseCallback", exception.Message, exception.StackTrace);
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (searchPageNumber > 1)
                {
                    //Opens web browser with bing search for the textbox contents as the search term, used as a backup when the Google search fails.
                    OpenBrowser(String.Format("http://www.bing.com/search?q={0}", searchQuery));
                }
            });
            SystemTray.ProgressIndicator.IsVisible = false;
        }

        /// <summary>
        /// Opens web browser with search for the textbox contents as the search term.
        /// </summary>
        /// <param name="searchQuery">the search terms in the textbox</param>
        public static void OpenBrowser(string searchQuery)
        {
            WebBrowserTask browser = new WebBrowserTask();
            browser.Uri = new Uri(searchQuery);
            browser.Show();
        }
    }
}
