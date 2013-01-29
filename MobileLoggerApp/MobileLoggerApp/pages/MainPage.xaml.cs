using System;
using System.Windows;
using Microsoft.Phone.Controls;
using MobileLoggerApp.src;

namespace MobileLoggerApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void sendPostToServer(object sender, RoutedEventArgs e)
        {
           // retrieve an avatar image from the Web
            Connection.PostTestCase(this);
            //string data = "no data yet";
            //navigateToPage(string.Format(PageLocations.responsePageUri + "?Val1={0}", data));
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