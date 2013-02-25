using Microsoft.Phone.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class OperatorHandler : AbstractLogHandler
    {
        JObject joOperator;

        DateTime lastSaved;

        public override void SaveSensorLog()
        {
            if (DeviceTools.SensorLastSavedTimeSpan(lastSaved))
            {
                SaveLogToDB(joOperator, "/log/operator");
                lastSaved = DateTime.UtcNow;
            }
        }

        public void startOperator()
        {
            if (joOperator == null)
            {
                joOperator = new JObject();
            }
            showOperator();
        }

        private void showOperator()
        {
            if (joOperator["operator"] == null)
            {
                if (DeviceNetworkInformation.CellularMobileOperator != null)
                {
                    joOperator.Add("operator", DeviceNetworkInformation.CellularMobileOperator.ToString());
                }
                else
                {
                    joOperator.Add("operator", "null");
                }
            }
            else
            {
                joOperator["operator"].Replace(DeviceNetworkInformation.CellularMobileOperator.ToString());
            }
        }
    }
}
