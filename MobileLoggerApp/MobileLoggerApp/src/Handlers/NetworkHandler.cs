using Microsoft.Phone.Net.NetworkInformation;
using MobileLoggerScheduledAgent.Devicetools;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace MobileLoggerApp.Handlers
{
    class NetworkHandler : AbstractLogHandler
    {
        public NetworkHandler()
        {
        }

        public override void SaveSensorLog()
        {
            SaveLogToDB(this.data, "/log/network");
        }

        public override void StartWatcher()
        {
            DeviceNetworkInformation.NetworkAvailabilityChanged += new EventHandler<NetworkNotificationEventArgs>(NetWorkAvailibilityChanged);
            UpdateNetworkValues();
            this.IsEnabled = true;
        }

        public override void StopWatcher()
        {
            DeviceNetworkInformation.NetworkAvailabilityChanged -= NetWorkAvailibilityChanged;
            this.IsEnabled = false;
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
            else
                NetworkNotAvailableMessageBox();
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

                AddJOValue("interfaceBandwidth", netInterfaceInfo.Bandwidth.ToString());
                AddJOValue("interfaceCharacteristics", netInterfaceInfo.Characteristics.ToString());
                AddJOValue("interfaceDescription", netInterfaceInfo.Description);
                AddJOValue("interfaceName", netInterfaceInfo.InterfaceName);
                AddJOValue("interfaceState", netInterfaceInfo.InterfaceState.ToString());
                AddJOValue("interfaceSubtype", netInterfaceInfo.InterfaceSubtype.ToString());
                AddJOValue("interfaceType", netInterfaceInfo.InterfaceType.ToString());
                AddJOValue("timestamp", DeviceTools.GetUnixTime());
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(e.SocketError.ToString(), "Error Getting Interface Information");

                AddJOValue("interfaceBandwidth", null);
                AddJOValue("interfaceCharacteristics", null);
                AddJOValue("interfaceDescription", null);
                AddJOValue("interfaceName", null);
                AddJOValue("interfaceState", null);
                AddJOValue("interfaceSubtype", null);
                AddJOValue("interfaceType", null);
                AddJOValue("timestamp", DeviceTools.GetUnixTime());
            }
        }

        public static void NetworkNotAvailableMessageBox()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show("Network is not available.");
            });
        }
    }
}
