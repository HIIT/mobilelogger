using System;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Phone.Controls;
using MobileLoggerApp.src;
using MobileLoggerApp.src.mobilelogger.model;

namespace MobileLoggerApp
{



    public partial class MainPage : PhoneApplicationPage
    {

        public const string ConnectionString = @"Data Source = 'isostore:/LogEventDB.sdf';";

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            using (LogEventDataContext logDBContext = new LogEventDataContext(ConnectionString))
            {

                if (!logDBContext.DatabaseExists())
                {
                    // create database if it does not exist
                    try
                    {
                        logDBContext.CreateDatabase();
                    }
                    catch (InvalidOperationException ioe) {
                        System.Diagnostics.Debug.WriteLine("InvalidOperationException while creating database..."+ioe);
                    }
                }

                logDBContext.addEvent(string.Format("Latitude: {0}, Longitude: {1}, Altitude: {2}", 0.1, 0.2, 0.3));
                
                IList<LogEvent> list = logDBContext.GetLogEvents();
                if (list != null)
                {

                    foreach (LogEvent e in list)
                    {
                        System.Diagnostics.Debug.WriteLine(e.ToString()+":"+e.sensorEvent);
                    }
                }
                
            }

        }

        private void sendPostToServer(object sender, RoutedEventArgs e)
        {
            string testiUri = "http://t-jonimake.users.cs.helsinki.fi/MobileLoggerServerDev/log";
            string testiViesti = "terve";
            string testiMetodi = "POST";
            Message msg = new Message(testiUri, testiViesti, testiMetodi, this);
            msg.SendMessage();
        }

        private void showGPSCoords(object sender, RoutedEventArgs e)
        {
            string coords = Application.Current.Resources["gpsHandler"].ToString();
            navigateToPage(string.Format( PageLocations.responsePageUri + "?Val1={0}", coords));
        }

        public void navigateToPage(string pageUri)
        {
            this.NavigationService.Navigate(new Uri(pageUri, UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            while(this.NavigationService.CanGoBack)
            {
                this.NavigationService.RemoveBackEntry();
            }
        }
    }
}