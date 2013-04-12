using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using MobileLoggerApp.Handlers;
using MobileLoggerApp.pages;
using MobileLoggerScheduledAgent.Database;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MobileLoggerApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        public delegate void KeyPressEventHandler(object sender, KeyEventArgs e);
        public delegate void KeyboardFocusHandler();
        public delegate void TouchEventHandler(MainPage mainPage, TouchFrameEventArgs e);

        public static event KeyPressEventHandler keyUp;

        public delegate void CustomPressedEventHandler(object sender, EventArgs e);

        public static event KeyboardFocusHandler keyboardGotFocus;
        public static event KeyboardFocusHandler keyboardLostFocus;

        public static event TouchEventHandler screenTouch;

        public const string ConnectionString = @"Data Source = 'isostore:/LogEventDB.sdf';";
        private const string TASK_NAME = "MobileLoggerScheduledAgent";
        private string searchTerm;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //start background agent
            StartAgent();
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
            }

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsLogDataLoaded)
                App.ViewModel.LoadLogData();

            if (!App.ViewModel.IsSettingsLoaded)
                App.ViewModel.LoadSettings();

            Touch.FrameReported += Touch_FrameReported;
        }

        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            screenTouch(this, e);
        }

        /// <summary>
        /// Saves an event to the local database
        /// </summary>
        /// <param name="logEvent">JSON representation of the event</param>
        /// <param name="url">relative url on the server where the event is sent to</param>
        /// <returns>false if database does not exist</returns>
        private Boolean SaveLogToDB(JObject logEvent, string url)
        {
            LogEventSaver.Instance.addEvent(logEvent, url);
            return true;
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

            WebBrowserTask browser = new WebBrowserTask();
            JObject link = JObject.Parse(item.LineThree);
            SaveLogToDB(link, "/log/clicked");
            browser.Uri = new Uri((string)link["link"]);
            browser.Show();
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            keyboardGotFocus();
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            keyboardLostFocus();
        }

        /// <summary>
        /// Event handler for search box, sends the search query to be queried from Google Custom Search
        /// </summary>
        /// <param name="sender">The object that initiated this event</param>
        /// <param name="e">Arguments for the key event that initiated this event</param>
        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            keyUp(sender, e);

            if (e.Key.Equals(Key.Enter))
            {
                this.Focus();
                searchTerm = SearchTextBox.Text;

                if (!searchTerm.Equals(""))
                {
                    GoogleSearch(1);
                    GetWeatherData();
                }
            }
            HandlersManager.SaveSensorLog();
        }

        /// <summary>
        /// Loads the next page of Google search results, up to 10 pages can be viewed
        /// </summary>
        /// <param name="sender">The button that initiated this event</param>
        /// <param name="e">Event arguments of the tap event</param>
        private void LoadNextPage(object sender, RoutedEventArgs e)
        {
            if (App.ViewModel.Items.Count >= 100 || App.ViewModel.Items.Count <= 0)
            {
                //nextPageButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                GoogleSearch(App.ViewModel.Items.Count + 1);
            }
        }

        /// <summary>
        /// Creates a Google Custom API search with the textbox contents as the search term
        /// </summary>
        private void GoogleSearch(int page)
        {
            GoogleCustomSearch search = new GoogleCustomSearch(this);
            search.Search(searchTerm, page);
        }

        private void GetWeatherData()
        {
            WeatherInformationSearch weatherInfo = new WeatherInformationSearch();
            weatherInfo.GetForecast();
        }

        /// <summary>
        /// Opens web browser with bing search for the textbox contents as the search term, used as a backup when the Google search fails
        /// </summary>
        /// <param name="searchQuery">the search terms in the textbox</param>
        internal void OpenBrowser(string searchQuery)
        {
            WebBrowserTask browser = new WebBrowserTask();
            browser.Uri = new Uri(String.Format("http://www.bing.com/search?q={0}", searchQuery));
            browser.Show();
        }

        private void currentPivotItem(object sender, SelectionChangedEventArgs e)
        {
            Pivot pivot = sender as Pivot;

            // Data PivotItem
            if (pivot.SelectedIndex == 1)
            {
                App.ViewModel.LoadLogData();
            }
        }

        private void StartAgent()
        {
            try
            {
                StopAgentIfStarted();

                PeriodicTask task = new PeriodicTask(TASK_NAME);
                task.ExpirationTime = DateTime.Now.AddDays(14);
                task.Description = "This is the background upload agent for MobileLoggerApp";
                // Place the call to Add in a try block in case the user has disabled agents.

                ScheduledActionService.Add(task);
            }
            catch (InvalidOperationException exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                }
                if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
                }
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
            }

#if DEBUG
            System.Diagnostics.Debug.WriteLine("DEBUG START AGENT");
            // If we're debugging, attempt to start the task immediately 
            try
            {
                ScheduledActionService.LaunchForTest(TASK_NAME, new TimeSpan(0, 0, 0));
            }
            catch (InvalidOperationException exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
#endif
        }

        private void StopAgentIfStarted()
        {
            try
            {
                if (ScheduledActionService.Find(TASK_NAME) != null)
                {
                    ScheduledAction task = ScheduledActionService.Find(TASK_NAME);
                    ScheduledActionService.Remove(TASK_NAME);
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void debugButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Debug send data");
            StartAgent();
        }

        private void HandlerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ListBoxItem checkedHandlerItem = this.SettingsListBox.ItemContainerGenerator.ContainerFromItem((sender as CheckBox).DataContext) as ListBoxItem;

            if (checkedHandlerItem != null)
            {
                HandlersManager.EnableHandler(GetHandlerName(checkedHandlerItem));
            }
        }

        private void HandlerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ListBoxItem checkedHandlerItem = this.SettingsListBox.ItemContainerGenerator.ContainerFromItem((sender as CheckBox).DataContext) as ListBoxItem;

            if (checkedHandlerItem != null)
            {
                HandlersManager.DisableHandler(GetHandlerName(checkedHandlerItem));
            }
        }

        private string GetHandlerName(ListBoxItem checkedHandlerItem)
        {
            ItemViewModel handlerItem = checkedHandlerItem.DataContext as ItemViewModel;
            return handlerItem.LineOne.ToString();
        }
    }
}
