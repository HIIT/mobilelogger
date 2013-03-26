using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    class SearchDataHandler : AbstractLogHandler
    {
        DateTime timestamp;

        public override void SaveSensorLog()
        {
        }

        public void StartSearchDataHandler()
        {
            ElGoog.searchDataEvent += new ElGoog.SearchDataHandler(SearchData);
        }

        private void SearchData(JObject searchData)
        {
            this.timestamp = DateTime.UtcNow;

            JArray searchResults = (JArray)searchData["items"];

            ProcessSearchData(searchData);

            if (searchResults != null)
            {
                JToken queries = searchData.GetValue("queries");
                JToken request = queries.Value<JToken>("request")[0];
                int offset = request.Value<int>("startIndex");

                ProcessSearchResults(searchResults, offset);
            }
        }

        private void ProcessSearchData(JObject searchData)
        {
            searchData.Remove("items");
            searchData.Add("time", DeviceTools.GetUnixTime(this.timestamp));
            searchData.Add("index", 0);
            SaveLogToDB(searchData, "/log/google");
        }

        private void ProcessSearchResults(JArray searchResults, int offset)
        {
            int index = 0;

            foreach (JToken result in searchResults)
            {
                JObject resultObj = result.ToObject<JObject>();

                resultObj.Add("index", index + offset);
                index++;
                resultObj.Add("time", DeviceTools.GetUnixTime(this.timestamp));

                if (resultObj.ToString().Length > 4000)
                {
                    resultObj.Remove("htmlFormattedUrl");
                    resultObj.Remove("htmlSnippet");
                    resultObj.Remove("htmlTitle");

                    if (resultObj.ToString().Length > 4000)
                    {
                        resultObj.Remove("pagemap");
                    }
                }
                SaveLogToDB(resultObj, "/log/google");
            }
        }
    }
}
