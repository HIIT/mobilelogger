using System;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Phone.Controls;
using MobileLoggerApp.src;
using MobileLoggerApp.src.mobilelogger.model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Input;
using MobileLoggerApp.pages;
using System.Windows.Controls;
using Microsoft.Phone.Tasks;

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

                //logDBContext.addEvent(string.Format("Latitude: {0}, Longitude: {1}, Altitude: {2}", 0.1, 0.2, 0.3));
                
                IList<LogEvent> list = logDBContext.GetLogEvents();
                if (list != null)
                {

                    foreach (LogEvent e in list)
                    {
                        System.Diagnostics.Debug.WriteLine(e.ToString()+":"+e.sensorEvent);
                    }
                }
                
            }


            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        //private void onKeyUp(KeyEventArgs e)
        //{
        //    if (e.Key.Equals(Key.Enter)) {
        //    }
        //}

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        //private void sendPostToServer(object sender, RoutedEventArgs e)
        //{
        //    ElGoog search = new ElGoog(this);
        //    search.Search("testi");
        //    /*string testiUri = "http://t-jonimake.users.cs.helsinki.fi/MobileLoggerServerDev/log";
        //    string testiViesti = "terve";
        //    string testiMetodi = "POST";
        //    Message msg = new Message(testiUri, testiViesti, testiMetodi, this);
        //    msg.SendMessage();*/
        //}

        /// <summary>
        /// Updates search results to screen
        /// </summary>
        /// <param name="JSON">JSON data in JObject form, must be in format from Google Custom Search</param>
        public void Update(JObject JSON)
        {
            JArray searchResults = (JArray)JSON["items"];
            App.ViewModel.Items.Clear();
            foreach (JToken t in searchResults)
            {
                System.Diagnostics.Debug.WriteLine(t["title"]);
                App.ViewModel.Items.Add(new ItemViewModel() { LineOne = (string)t["title"], LineTwo = (string)t["snippet"], LineThree = (string)t["link"] });
            }
        }

        /// <summary>
        /// An event handler for tapping a result, opens the result in Internet Explorer
        /// </summary>
        /// <param name="sender">The StackPanel object that was tapped</param>
        /// <param name="e">Event arguments of the tap event</param>
        private void itemTappedEvent(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            ItemViewModel item = (ItemViewModel)stackPanel.DataContext;
            System.Diagnostics.Debug.WriteLine(item.LineThree);
            WebBrowserTask browser = new WebBrowserTask();
            browser.Uri = new Uri(item.LineThree);
            browser.Show();
        }

        /// <summary>
        /// Event handler for search box, sends the search query to be queried from Google Custom Search
        /// </summary>
        /// <param name="sender">The object that initiated this event</param>
        /// <param name="e">Arguments for the key event that initiated this event</param>
        private void SearchTextBox_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                ElGoog search = new ElGoog(this);
                search.Search(SearchTextBox.Text);

                MessagingService serv = new MessagingService();
                serv.SendMessages();

                this.Focus();

            }
        }

        //private void showGPSCoords(object sender, RoutedEventArgs e)
        //{
        //    string coords = Application.Current.Resources["gpsHandler"].ToString();
        //    navigateToPage(string.Format( PageLocations.responsePageUri + "?Val1={0}", coords));
        //}

        //public void navigateToPage(string pageUri)
        //{
        //    this.NavigationService.Navigate(new Uri(pageUri, UriKind.Relative));
        //}

        //protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        //{
        //    while(this.NavigationService.CanGoBack)
        //    {
        //        this.NavigationService.RemoveBackEntry();
        //    }
        //}

        internal void OpenBrowser(string searchQuery)
        {
            WebBrowserTask browser = new WebBrowserTask();
            browser.Uri = new Uri(String.Format("http://www.bing.com/search?q={0}", searchQuery));
            browser.Show();
        }
    }
}