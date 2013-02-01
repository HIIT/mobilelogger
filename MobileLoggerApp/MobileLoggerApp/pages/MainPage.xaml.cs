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
            string testiUri = "http://t-jonimake.users.cs.helsinki.fi/MobileLoggerServerDev/log";
            string testiViesti = "terve";
            string testiMetodi = "POST";
            Message msg = new Message(testiUri, testiViesti, testiMetodi, this);
            msg.SendMessage();
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