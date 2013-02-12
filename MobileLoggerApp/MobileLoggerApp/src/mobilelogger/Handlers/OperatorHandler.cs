using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Phone.Net.NetworkInformation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class OperatorHandler : AbstractLogHandler
    {
        JObject joOperator;

        public override void SaveSensorLog()
        {
            SaveLogToDB(joOperator, "/log/operator");
        }

        public void startOperator()
        {
            if (joOperator == null)
            {
                joOperator = new JObject();
            }
            show_operator();
        }
        private void show_operator()
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
                joOperator["operator"]. Replace(DeviceNetworkInformation.CellularMobileOperator.ToString());
            }
            System.Diagnostics.Debug.WriteLine(joOperator["operator"]);
        }
    }
}
    
