using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows;
using System;
using MobileLoggerApp.src;

namespace MobileLoggerApp.pages
{
    public partial class ResponsePage : PhoneApplicationPage
    {
        public ResponsePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string data = NavigationContext.QueryString["Val1"];
            Response.Text = data;
            base.OnNavigatedTo(e);
        }

        public void UpdateText(string data)
        {
            Response.Text = data;
        }

        private void returnToMainPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri(string.Format(PageLocations.mainPageUri), UriKind.Relative));
        }
    }
}