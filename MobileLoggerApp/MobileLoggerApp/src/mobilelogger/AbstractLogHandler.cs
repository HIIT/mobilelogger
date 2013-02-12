﻿using MobileLoggerApp.src.mobilelogger.model;
using Newtonsoft.Json.Linq;
using System;

namespace MobileLoggerApp.src.mobilelogger
{
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
