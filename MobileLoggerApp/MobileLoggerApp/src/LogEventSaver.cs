using MobileLoggerScheduledAgent.Database;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileLoggerApp
{
    public class LogEventSaver
    {
        private Queue<LogEvent> saveQueue;
        private static LogEventSaver instance;

        private LogEventSaver() 
        { 
            saveQueue = new Queue<LogEvent>(); 
        }

        public static LogEventSaver Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogEventSaver();
                }
                return instance;
            }
        }

        public void SaveAll()
        {
            lock (saveQueue) { 
                using (LogEventDataContext logDBContext = new LogEventDataContext(MainPage.ConnectionString))
                {
                
                    logDBContext.addEvents(saveQueue.ToList().AsReadOnly());
                    saveQueue = new Queue<LogEvent>();
                    System.Diagnostics.Debug.WriteLine("Batch insert done");
                }
            }
        }

        public void addEvent(string data, string url)
        {
            System.Diagnostics.Debug.WriteLine(data);
            System.Diagnostics.Debug.WriteLine(url);
            LogEvent e = new LogEvent();
            e.relativeUrl = url;
            e.sensorEvent = data;
            saveQueue.Enqueue(e);
        }
    }
}
