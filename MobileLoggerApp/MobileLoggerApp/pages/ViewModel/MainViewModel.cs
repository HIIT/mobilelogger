using MobileLoggerApp.Handlers;
using MobileLoggerScheduledAgent.Database;
using MobileLoggerScheduledAgent.Devicetools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;

namespace MobileLoggerApp.pages
{
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<SearchResults> SearchResults { get; set; }
        public ObservableCollection<LogData> LogData { get; set; }
        public ObservableCollection<HandlerSettings> HandlerSettings { get; set; }

        public MainViewModel()
        {
            this.SearchResults = new ObservableCollection<SearchResults>();
            this.LogData = new ObservableCollection<LogData>();
            //this.Settings = new ObservableCollection<HandlerSettings>();
        }

        public bool IsLogDataLoaded
        {
            get;
            private set;
        }

        public bool IsSettingsLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void GetLogData()
        {
            const string ConnectionString = @"Data Source = 'isostore:/LogEventDB.sdf';";

            using (LogEventDataContext logDBContext = new LogEventDataContext(ConnectionString))
            {
                if (!logDBContext.DatabaseExists())
                {
                    // create database if it does not exist
                    try
                    {
                        logDBContext.CreateDatabase();
                    }
                    catch (InvalidOperationException ioe)
                    {
                        System.Diagnostics.Debug.WriteLine("InvalidOperationException while creating database..." + ioe);
                    }
                }
                GetLogEvents(logDBContext);
            }
        }

        private void GetLogEvents(LogEventDataContext logDBContext)
        {
            List<LogEvent> list = logDBContext.GetLogEvents();

            if (list != null)
            {
                LogData.Clear();
                int listCount = list.Count;

                for (int i = listCount - 1; i >= 0; i--)
                {
                    LogEvent e = list[i];
                    LogData.Add(new LogData()
                    {
                        LogDataHeading = DeviceTools.GetDateTime(e.Time).ToString(),
                        LogDataContent = e.sensorEvent.ToString()
                    });
                }
                this.IsLogDataLoaded = true;
            }
        }

        public void GetHandlerSettings()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("HandlerSettings"))
                GetSavedHandlerSettings();
            else
                GetDefaultHandlerSettings();
        }

        private void GetSavedHandlerSettings()
        {
            IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
            ObservableCollection<HandlerSettings> handlerSettings = new ObservableCollection<HandlerSettings>();
            Dictionary<string, bool> handlers;
            appSettings.TryGetValue("HandlerSettings", out handlers);

            if (handlers != null)
            {
                foreach (KeyValuePair<string, bool> handler in handlers)
                {
                    handlerSettings.Add(new HandlerSettings() { HandlerName = handler.Key, HandlerIsChecked = handler.Value });
                }
                HandlerSettings = handlerSettings;
            }
        }

        private void GetDefaultHandlerSettings()
        {
            IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
            ObservableCollection<HandlerSettings> handlerSettings = new ObservableCollection<HandlerSettings>();
            Dictionary<String, bool> handlers = new Dictionary<string, bool>();

            foreach (KeyValuePair<string, AbstractLogHandler> logHandler in HandlersManager.LogHandlers)
            {
                handlerSettings.Add(new HandlerSettings() { HandlerName = logHandler.Key, HandlerIsChecked = true });
                handlers.Add(logHandler.Key, true);
            }
            appSettings.Add("HandlerSettings", handlers);
            HandlerSettings = handlerSettings;
        }

        public void GetSearchResults(JArray searchResults, Boolean newSearch)
        {
            if (newSearch)
                SearchResults.Clear();

            foreach (JToken searchResult in searchResults)
                if (SearchResultHasLink(searchResult))
                    SearchResults.Add(new SearchResults()
                    {
                        SearchResultTitle = searchResult.SelectToken("title").ToString(),
                        SearchResultSnippet = searchResult.SelectToken("snippet").ToString(),
                        SearchResultLink = searchResult.SelectToken("link").ToString(),
                        SearchResult = searchResult as JObject
                    });
        }

        private bool SearchResultHasLink(JToken searchResult)
        {
            bool hasLink = true;
            searchResult.SelectToken("link", hasLink);

            return hasLink;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
