using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using MobileLoggerApp.src.mobilelogger.model;
using System.Text;

namespace MobileLoggerApp.src.mobilelogger
{

    [Obsolete("Need to refactor this class to produce events that are saved into the DB, sending is handled by the MessagingService")]
    public abstract class AbstractLogHandler
    {
        public abstract void SaveSensorLog();

        protected Boolean SaveLogToDB(JObject logEvent, string url) 
        {
            using (LogEventDataContext logDBContext = new LogEventDataContext(MainPage.ConnectionString))
            {

                if (!logDBContext.DatabaseExists()) return false;

                logDBContext.addEvent(logEvent.ToString(), url);
            }

            return true;
        }

    }
}
