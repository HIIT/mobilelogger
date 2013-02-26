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

        public void StartOperator()
        {
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
                AddJOValue("operator", DeviceNetworkInformation.CellularMobileOperator.ToString());
            }
            else
            {
                AddJOValue("operator", "null");
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
