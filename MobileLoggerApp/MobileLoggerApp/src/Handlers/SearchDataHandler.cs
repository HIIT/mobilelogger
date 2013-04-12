using MobileLoggerScheduledAgent.Devicetools;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.Handlers
{
    class SearchDataHandler : AbstractLogHandler
    {
        private static string URL = "/log/google";
        string[] searchResultRemovableItems = {"htmlFormattedUrl", "htmlSnippet", "htmlTitle", "pagemap"};
        int localDBStringMaxLength = DeviceTools.GetDeviceLocalDBStringMaxLength();

        public SearchDataHandler()
        {
            this.IsEnabled = true;
        }

        public override void SaveSensorLog()
        {
            //handle saving in the event handler method below
        }

        public override void StartWatcher()
        {
            GoogleCustomSearch.searchDataEvent += new GoogleCustomSearch.SearchDataHandler(SearchData);
            MainPage.searchResultTap += new MainPage.SearchResultTapped(SearchResultTapped);
        }

        public override void StopWatcher()
        {
            GoogleCustomSearch.searchDataEvent -= this.SearchData;
            MainPage.searchResultTap -= this.SearchResultTapped;
        }

        private void SearchData(JObject searchData)
        {
            DateTime timestamp = DateTime.UtcNow;
            JArray searchResults = (JArray)searchData["items"];

            ProcessSearchData(searchData, timestamp);

            if (searchResults != null)
            {
                JToken queries = searchData.GetValue("queries");
                JToken request = queries.Value<JToken>("request")[0];
                int offset = request.Value<int>("startIndex");

                ProcessSearchResults(searchResults, offset, timestamp);
            }
        }

        private void ProcessSearchData(JObject searchData, DateTime timestamp)
        {
            this.data = searchData;
            this.data.Remove("items");
            AddJOValue("timestamp", DeviceTools.GetUnixTime(timestamp));
            AddJOValue("index", 0);
            SaveLogToDB(this.data, URL);
        }

        private void ProcessSearchResults(JArray searchResults, int offset, DateTime timestamp)
        {
            int index = 0;

            foreach (JToken result in searchResults)
            {
                JObject resultObj = result.ToObject<JObject>();

                resultObj.Add("index", index + offset);
                index++;
                resultObj.Add("timestamp", DeviceTools.GetUnixTime(timestamp));

                ParseSearchResult(resultObj);
                SaveLogToDB(resultObj, URL);
            }
        }

        /// <summary>
        /// This method parses the search result because device's local DB can only handle strings shorter than 4000 characters.
        /// </summary>
        private void ParseSearchResult(JObject resultObj)
        {
            int removableItemIndex = 0;

            while (resultObj.ToString().Length > localDBStringMaxLength)
            {
                resultObj.Remove(searchResultRemovableItems[removableItemIndex]);
                if (removableItemIndex < searchResultRemovableItems.Length)
                    removableItemIndex++;
                else
                    break;
            }
        }

        private void SearchResultTapped(JObject searchResult)
        {
            SaveLogToDB(searchResult, "/log/clicked");
        }
    }
}
