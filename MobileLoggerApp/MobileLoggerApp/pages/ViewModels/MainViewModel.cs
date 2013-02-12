﻿using MobileLoggerApp.src;
using MobileLoggerApp.src.mobilelogger.model;
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
            this.Settings = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }
        public ObservableCollection<ItemViewModel> Settings { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
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

                //logDBContext.addEvent(string.Format("Latitude: {0}, Longitude: {1}, Altitude: {2}", 0.1, 0.2, 0.3));

                IList<LogEvent> list = logDBContext.GetLogEvents();
                if (list != null)
                {
                    App.ViewModel.Settings.Clear();

                    foreach (LogEvent e in list)
                    {
                        App.ViewModel.Settings.Add(new ItemViewModel() { LineOne = DeviceTools.GetDateTime(e.Time).ToString(), LineThree = e.sensorEvent.ToString() });
                    }
                    this.IsDataLoaded = true;
                }
            }
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