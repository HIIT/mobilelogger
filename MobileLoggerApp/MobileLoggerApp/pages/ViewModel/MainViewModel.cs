﻿using Microsoft.Phone.Shell;
using MobileLogger;
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
using System.Linq;

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
        public ObservableCollection<ApplicationInfo> ApplicationInfo { get; set; }

        public MainViewModel()
        {
            this.LogData = new ObservableCollection<LogData>();
            this.Results = new ObservableCollection<SearchResults>();
            this.HandlerSettings = new ObservableCollection<HandlerSettings>();
            this.ApplicationInfo = new ObservableCollection<ApplicationInfo>();
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

            IsolatedStorageSettings.ApplicationSettings.TryGetValue("HandlerSettings", out savedHandlers);

            foreach (HandlerSettings handler in savedHandlers)
            {
                handlerSettings.Add(new HandlerSettings() {
                    HandlerName = handler.HandlerName,
                    HandlerIsChecked = handler.HandlerIsChecked });
            }
            HandlerSettings = handlerSettings;
            IsolatedStorageSettings.ApplicationSettings["HandlerSettings"] = HandlerSettings;
        }

        private void GetDefaultHandlerSettings()
        {
            ObservableCollection<HandlerSettings> handlerSettings = new ObservableCollection<HandlerSettings>();

            Dictionary<string, bool> handlerState = new Dictionary<string, bool>();
            bool isHandlerEnabled;

            if (PhoneApplicationService.Current.State.ContainsKey("Handlers"))
            {
                handlerState = PhoneApplicationService.Current.State["Handlers"] as Dictionary<string, bool>;
            }

            foreach (KeyValuePair<string, AbstractLogHandler> logHandler in HandlersManager.LogHandlers)
            {
                if (handlerState.Count > 0)
                {
                    handlerState.TryGetValue(logHandler.Key, out isHandlerEnabled);
                    handlerSettings.Add(new HandlerSettings() {
                        HandlerName = logHandler.Key,
                        HandlerIsChecked = isHandlerEnabled });
                }
                else
                {
                    handlerSettings.Add(new HandlerSettings() {
                        HandlerName = logHandler.Key,
                        HandlerIsChecked = StateUtilities.StartHandlers });
                }
            }
            HandlerSettings = handlerSettings;
            IsolatedStorageSettings.ApplicationSettings.Add("HandlerSettings", HandlerSettings);
        }

        public void SaveHandlerSettings()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("HandlerSettings"))
                IsolatedStorageSettings.ApplicationSettings["HandlerSettings"] = HandlerSettings;
            else
                IsolatedStorageSettings.ApplicationSettings.Add("HandlerSettings", HandlerSettings);
        }

        public void GetAppInfo()
        {

            string appVersion = (from manifest in System.Xml.Linq.XElement.Load("WMAppManifest.xml").Descendants("App")
                                 select manifest).SingleOrDefault().Attribute("Version").Value;

            ObservableCollection<ApplicationInfo> appInfo = new ObservableCollection<ApplicationInfo>();

            appInfo.Add(new ApplicationInfo() { AppName = "Name: MobileLogger" });
            appInfo.Add(new ApplicationInfo() { CurrentVersion = "Version: " + appVersion });
            appInfo.Add(new ApplicationInfo() { ContactName = "Technical support: antti.ukkonen@hiit.fi" });
            appInfo.Add(new ApplicationInfo() { ContactNumber = "Phone: +358 50 407 0576" });

            ApplicationInfo = appInfo;
        }

        public void GetSearchResults(JArray searchResultsList)
        {
            if (StateUtilities.NewSearch)
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
