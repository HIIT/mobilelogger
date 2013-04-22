using MobileLoggerApp.Handlers;
using MobileLoggerScheduledAgent.Database;
using MobileLoggerScheduledAgent.Devicetools;
using Newtonsoft.Json;
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
        public ObservableCollection<SearchResults> Results { get; set; }
        public ObservableCollection<LogData> LogData { get; set; }
        public ObservableCollection<HandlerSettings> HandlerSettings { get; set; }
        IsolatedStorageSettings appSettings;

        public MainViewModel()
        {
            this.LogData = new ObservableCollection<LogData>();
            this.appSettings = IsolatedStorageSettings.ApplicationSettings;
            this.Results = new ObservableCollection<SearchResults>();
            this.HandlerSettings = new ObservableCollection<HandlerSettings>();
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
            ObservableCollection<HandlerSettings> handlerSettings = new ObservableCollection<HandlerSettings>();
            ObservableCollection<HandlerSettings> savedHandlers;

            appSettings.TryGetValue("HandlerSettings", out savedHandlers);

            foreach (HandlerSettings handler in savedHandlers)
            {
                handlerSettings.Add(new HandlerSettings() { HandlerName = handler.HandlerName, HandlerIsChecked = handler.HandlerIsChecked });
            }
            HandlerSettings = handlerSettings;
            appSettings["HandlerSettings"] = HandlerSettings;
        }

        private void GetDefaultHandlerSettings()
        {
            ObservableCollection<HandlerSettings> handlerSettings = new ObservableCollection<HandlerSettings>();

            foreach (KeyValuePair<string, AbstractLogHandler> logHandler in HandlersManager.LogHandlers)
            {
                handlerSettings.Add(new HandlerSettings() { HandlerName = logHandler.Key, HandlerIsChecked = true });
            }
            HandlerSettings = handlerSettings;
            appSettings.Add("HandlerSettings", HandlerSettings);
        }

        public void SaveHandlerSettings()
        {
            if (appSettings.Contains("HandlerSettings"))
                appSettings["HandlerSettings"] = HandlerSettings;
            else
                appSettings.Add("HandlerSettings", HandlerSettings);
        }

        public void GetSearchResults(JArray searchResultsList, Boolean newSearch)
        {
            if (newSearch)
                Results.Clear();

            foreach (JToken searchResult in searchResultsList)
                if (SearchResultHasLink(searchResult))
                    Results.Add(new SearchResults()
                    {
                        SearchResultTitle = searchResult.SelectToken("title").ToString(),
                        SearchResultSnippet = searchResult.SelectToken("snippet").ToString(),
                        SearchResultLink = searchResult.SelectToken("link").ToString(),
                        SearchResult = searchResult.ToString(Formatting.None)
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
