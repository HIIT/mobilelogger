using Microsoft.Phone.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Sockets;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class NetworkHandler : AbstractLogHandler
    {
        JObject joNetwork;

        public override void SaveSensorLog()
        {
        }

        public void StartNetworkInformation()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string serverName = "www.google.com";
            int portNumber = 7;

            DnsEndPoint hostEntry = new DnsEndPoint(serverName, portNumber);

            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = hostEntry;
            socketEventArg.UserToken = socket;
            socketEventArg.Completed += ShowNetworkInterfaceInformation;

            socket.ConnectAsync(socketEventArg);

            if (joNetwork == null)
            {
                joNetwork = new JObject();
            }
        }

        void ShowNetworkInterfaceInformation(object sender, SocketAsyncEventArgs e)
        {
            Socket socket = e.UserToken as Socket;

            if (e.SocketError == SocketError.Success)
            {
                NetworkInterfaceInfo netInterfaceInfo = socket.GetCurrentNetworkInterface();

                AddJOValue("interfaceName", netInterfaceInfo.InterfaceName);
                AddJOValue("interfaceState", netInterfaceInfo.InterfaceState.ToString());
                AddJOValue("interfaceType", netInterfaceInfo.InterfaceType.ToString());
                AddJOValue("interfaceSubType", netInterfaceInfo.InterfaceSubtype.ToString());
            }
            else
            {
                AddJOValue("interfaceName", "null");
                AddJOValue("interfaceState", "null");
                AddJOValue("interfaceType", "null");
                AddJOValue("interfaceSubType", "null");
            }
            socket.Close();
        }

        private void AddJOValue(String key, String value)
        {
            if (joNetwork[key] == null)
            {
                joNetwork.Add(key, value);
            }
            else
            {
                joNetwork[key].Replace(value);
            }
        }
    }
}
