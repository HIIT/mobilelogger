using Microsoft.Phone.Shell;
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
            this.Items = new ObservableCollection<ItemViewModel>();
            this.LogData = new ObservableCollection<ItemViewModel>();
            this.Settings = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }
        public ObservableCollection<ItemViewModel> LogData { get; private set; }
        public ObservableCollection<ItemViewModel> Settings { get; private set; }

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
                    LogData.Add(new ItemViewModel() { LineOne = DeviceTools.GetDateTime(e.Time).ToString(), LineThree = e.sensorEvent.ToString() });
                }
                this.IsLogDataLoaded = true;
            }
        }

        public void LoadSettings()
        {
            foreach (KeyValuePair<string, AbstractLogHandler> logHandler in HandlersManager.logHandlers)
                Settings.Add(new ItemViewModel() { LineOne = logHandler.Key, IsChecked = logHandler.Value.IsEnabled });

            this.IsSettingsLoaded = true;
        }

        public void UpdateSearchResults(JArray searchResults, Boolean reset)
        {
            if (reset)
            {
                Items.Clear();
            }
            foreach (JToken result in searchResults)
            {
                Items.Add(new ItemViewModel() { LineOne = (string)result["title"], LineTwo = (string)result["snippet"], LineThree = result.ToString() });
            }
            SystemTray.ProgressIndicator.IsVisible = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
