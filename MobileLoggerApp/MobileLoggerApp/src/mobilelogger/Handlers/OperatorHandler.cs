using Microsoft.Phone.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class OperatorHandler : AbstractLogHandler
    {
        JObject joOperator;

        public override void SaveSensorLog()
        {
            SaveLogToDB(joOperator, "/log/operator");
        }

        public void StartOperator()
        {
            DeviceNetworkInformation.NetworkAvailabilityChanged += new EventHandler<NetworkNotificationEventArgs>(NetWorkAvailibilityChanged);

            if (joOperator == null)
            {
                joOperator = new JObject();
            }
            ShowOperator();
        }

        private void ShowOperator()
        {
            if (DeviceNetworkInformation.CellularMobileOperator != null)
            {
                string operatorName = DeviceNetworkInformation.CellularMobileOperator.ToString();

                AddJOValue("operator", operatorName);
            }

            if (DeviceNetworkInformation.IsWiFiEnabled)
            {
                NetworkInterfaceList networkInterfaceList = new NetworkInterfaceList();

                foreach (NetworkInterfaceInfo networkInterfaceInfo in networkInterfaceList)
                {
                    System.Diagnostics.Debug.WriteLine(networkInterfaceInfo.InterfaceName);
                }
            }
        }

        void NetWorkAvailibilityChanged(object sender, NetworkNotificationEventArgs e)
        {
            switch (e.NotificationType)
            {
                case NetworkNotificationType.InterfaceConnected:
                    if (DeviceNetworkInformation.IsCellularDataEnabled)
                    {
                        AddJOValue("operator", DeviceNetworkInformation.CellularMobileOperator.ToString());
                    }
                    if (DeviceNetworkInformation.IsWiFiEnabled)
                    {
                        NetworkInterfaceList networkInterfaceList = new NetworkInterfaceList();

                        foreach (NetworkInterfaceInfo networkInterfaceInfo in networkInterfaceList)
                        {
                            System.Diagnostics.Debug.WriteLine(networkInterfaceInfo.InterfaceName);
                        }
                    }
                    break;
                case NetworkNotificationType.InterfaceDisconnected:
                    break;
                case NetworkNotificationType.CharacteristicUpdate:
                    break;
                default:
                    break;
            }
        }

        private void AddJOValue(String key, String value)
        {
            if (joOperator[key] == null)
            {
                joOperator.Add(key, value);
            }
            else
            {
                joOperator[key].Replace(value);
            }
        }
    }
}
