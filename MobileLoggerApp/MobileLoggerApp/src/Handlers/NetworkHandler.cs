using Microsoft.Phone.Net.NetworkInformation;
using MobileLoggerScheduledAgent.Devicetools;
using System;
using System.Net;
using System.Net.Sockets;

namespace MobileLoggerApp.Handlers
{
    class NetworkHandler : AbstractLogHandler
    {
        public NetworkHandler()
        {
            this.IsEnabled = true;
        }

        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, "/log/network");
        }

        public void StartNetworkWatcher()
        {
            DeviceNetworkInformation.NetworkAvailabilityChanged += new EventHandler<NetworkNotificationEventArgs>(NetWorkAvailibilityChanged);

            UpdateNetworkValues();
        }

        public void StopNetWorkWatcher()
        {
            DeviceNetworkInformation.NetworkAvailabilityChanged -= NetWorkAvailibilityChanged;
        }

        private void NetWorkAvailibilityChanged(object sender, NetworkNotificationEventArgs e)
        {
            switch (e.NotificationType)
            {
                case NetworkNotificationType.InterfaceConnected:
                    UpdateNetworkValues();
                    break;
                case NetworkNotificationType.InterfaceDisconnected:
                    UpdateNetworkValues();
                    break;
                case NetworkNotificationType.CharacteristicUpdate:
                    UpdateNetworkValues();
                    break;
                default:
                    break;
            }
        }

        private void UpdateNetworkValues()
        {
            if (DeviceNetworkInformation.IsNetworkAvailable)
            {
                UpdateCellularMobileOperator();
                NetworkInterfaceInformation();
            }
        }

        private void UpdateCellularMobileOperator()
        {
            if (DeviceNetworkInformation.IsCellularDataEnabled)
            {
                if (DeviceNetworkInformation.CellularMobileOperator != null)
                {
                    AddJOValue("operator", DeviceNetworkInformation.CellularMobileOperator.ToString());
                    AddJOValue("timestamp", DeviceTools.GetUnixTime());
                }
                else
                {
                    AddJOValue("operator", null);
                    AddJOValue("timestamp", DeviceTools.GetUnixTime());
                }
            }
        }

        private void NetworkInterfaceInformation()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string serverName = "173.194.69.104";
            int portNumber = 80;

            DnsEndPoint hostEntry = new DnsEndPoint(serverName, portNumber);

            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = hostEntry;
            socketEventArg.UserToken = socket;
            socketEventArg.Completed += UpdateNetworkInterfaceInformation;

            socket.ConnectAsync(socketEventArg);
        }

        void UpdateNetworkInterfaceInformation(object sender, SocketAsyncEventArgs e)
        {
            Socket socket = e.UserToken as Socket;

            if (e.SocketError == SocketError.Success)
            {
                NetworkInterfaceInfo netInterfaceInfo = socket.GetCurrentNetworkInterface();

                AddJOValue("InterfaceBandwidth", netInterfaceInfo.Bandwidth.ToString());
                AddJOValue("InterfaceCharacteristics", netInterfaceInfo.Characteristics.ToString());
                AddJOValue("InterfaceDescription", netInterfaceInfo.Description);
                AddJOValue("InterfaceName", netInterfaceInfo.InterfaceName);
                AddJOValue("InterfaceState", netInterfaceInfo.InterfaceState.ToString());
                AddJOValue("InterfaceSubtype", netInterfaceInfo.InterfaceSubtype.ToString());
                AddJOValue("InterfaceType", netInterfaceInfo.InterfaceType.ToString());
                AddJOValue("timestamp", DeviceTools.GetUnixTime());
            }
            else
            {
                AddJOValue("InterfaceBandwidth", null);
                AddJOValue("InterfaceCharacteristics", null);
                AddJOValue("InterfaceDescription", null);
                AddJOValue("InterfaceName", null);
                AddJOValue("InterfaceState", null);
                AddJOValue("InterfaceSubtype", null);
                AddJOValue("InterfaceType", null);
                AddJOValue("timestamp", DeviceTools.GetUnixTime());
            }
        }
    }
}
