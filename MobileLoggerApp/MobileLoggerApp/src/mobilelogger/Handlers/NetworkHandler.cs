using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using Microsoft.Phone.Net.NetworkInformation;
using System.Threading;
using System.Net;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class NetworkHandler : AbstractLogHandler
    {
        JObject joNetwork;

        public override void SaveSensorLog()
        {
        }

        public void startNetworkiInformation()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string serverName = "http://www.google.com";
            int portNumber = 7;

            DnsEndPoint hostEntry = new DnsEndPoint(serverName, portNumber);

            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = hostEntry;
            socketEventArg.UserToken = socket;
            socketEventArg.Completed += ShowNetworkInterfaceInformation;

            socket.ConnectAsync(socketEventArg);
        }

        void ShowNetworkInterfaceInformation(object sender, SocketAsyncEventArgs e)
        {
            Socket socket = e.UserToken as Socket;

            if (e.SocketError == SocketError.Success)
            {
                NetworkInterfaceInfo netInterfaceInfo = socket.GetCurrentNetworkInterface();

                StringBuilder sb = new StringBuilder();
            }
        }
    }
}

