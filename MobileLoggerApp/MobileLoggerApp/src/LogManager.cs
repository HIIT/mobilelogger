using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileLoggerApp.src.mobilelogger;


namespace MobileLoggerApp.src
{
    class LogManager
    {
        private List<AbstractLogHandler> logHandlers;

        public LogManager()
        {
            logHandlers = new List<AbstractLogHandler>();
        }
    }
}
