using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileLoggerApp.src.mobilelogger;


namespace MobileLoggerApp.src
{
    public class LogManager
    {
        private List<AbstractLogHandler> logHandlers;

        public LogManager()
        {
            logHandlers = new List<AbstractLogHandler>();
        }

        public void addHandler(AbstractLogHandler alh)
        {
            logHandlers.Add(alh);
        }

        public void sendAllData()
        {
            foreach (AbstractLogHandler alh in logHandlers)
            {
                alh.SaveSensorLog();
            }
        }
    }
}
