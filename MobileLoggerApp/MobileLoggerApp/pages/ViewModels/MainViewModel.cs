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
            this.SearchResults = new ObservableCollection<SearchResultsViewModel>();
            this.LogData = new ObservableCollection<LogDataViewModel>();
            this.Settings = new ObservableCollection<SettingsViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<SearchResultsViewModel> SearchResults { get; set; }
        public ObservableCollection<LogDataViewModel> LogData { get; set; }
        public ObservableCollection<SettingsViewModel> Settings { get; set; }

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
                    LogData.Add(new LogDataViewModel()
                    {
                        DataLineOne = DeviceTools.GetDateTime(e.Time).ToString(),
                        DataLineTwo = e.sensorEvent.ToString()
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
                    Settings.Add(new SettingsViewModel() { SettingsLineOne = logHandler.Key, SettingsIsChecked = logHandler.Value.IsEnabled });

                this.IsSettingsLoaded = true;
            }
        }

        public void LoadSearchResults(JArray searchResults, Boolean newSearch)
        {
            if (newSearch)
                SearchResults.Clear();

            foreach (JToken searchResult in searchResults)
                if (SearchResultHasLink(searchResult))
                    SearchResults.Add(new SearchResultsViewModel()
                    {
                        SearchLineOne = searchResult.SelectToken("title").ToString(),
                        SearchLineTwo = searchResult.SelectToken("snippet").ToString(),
                        SearchLineThree = searchResult.SelectToken("link").ToString(),
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
