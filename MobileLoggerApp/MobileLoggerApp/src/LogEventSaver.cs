using MobileLoggerScheduledAgent.Database;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileLoggerApp
{
    public class LogEventSaver
    {
        private Queue<LogEvent> saveQueue = new Queue<LogEvent>();

        private static LogEventSaver instance;

        private LogEventSaver() { }

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
            using (LogEventDataContext logDBContext = new LogEventDataContext(MainPage.ConnectionString))
            {
                while (saveQueue.Any()) //while !isEmpty
                {
                    LogEvent e = saveQueue.Dequeue();
                    logDBContext.addEvent(e.data.ToString(), e.url);
                }
            }
        }

        public void addEvent(JObject data, string url)
        {
            saveQueue.Enqueue(new LogEvent(data, url));
        }

        private class LogEvent
        {
            public LogEvent(JObject data, String url)
            {
                this.data = data;
                this.url = url;
            }
            public JObject data { get; set; }
            public String url { get; set; }
        }
    }
}
