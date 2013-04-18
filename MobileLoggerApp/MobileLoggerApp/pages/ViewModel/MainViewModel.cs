using MobileLoggerApp.Handlers;
using MobileLoggerScheduledAgent.Database;
using MobileLoggerScheduledAgent.Devicetools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MobileLoggerApp.pages
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.SearchResults = new ObservableCollection<SearchResults>();
            this.LogData = new ObservableCollection<LogData>();
            this.Settings = new ObservableCollection<HandlerSettings>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<SearchResults> SearchResults { get; set; }
        public ObservableCollection<LogData> LogData { get; set; }
        public ObservableCollection<HandlerSettings> Settings { get; set; }

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
        public void LoadLogData()
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
                LoadLogEvents(logDBContext);
            }
        }

        private void LoadLogEvents(LogEventDataContext logDBContext)
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

        public void LoadSettings()
        {
            if (HandlersManager.LogHandlers != null)
            {
                foreach (KeyValuePair<string, AbstractLogHandler> logHandler in HandlersManager.LogHandlers)
                    Settings.Add(new HandlerSettings() { HandlerName = logHandler.Key, HandlerIsChecked = logHandler.Value.IsEnabled });

                this.IsSettingsLoaded = true;
            }
        }

        public void LoadSearchResults(JArray searchResults, Boolean newSearch)
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
