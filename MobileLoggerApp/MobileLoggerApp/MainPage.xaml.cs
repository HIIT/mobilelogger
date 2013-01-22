using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

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
           string avatarUri = "http://t-jonimake.users.cs.helsinki.fi/MobileLoggerServerDev/postFromPhone";
           HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(avatarUri);
           request.Method = "POST";
           request.BeginGetResponse(responseCallBack, request);
        }

        private void responseCallBack(IAsyncResult asyncResult)
        {
            Console.WriteLine(asyncResult.ToString());
        }
    }
}