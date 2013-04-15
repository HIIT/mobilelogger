using MobileLoggerScheduledAgent.Database;
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
            lock (saveQueue)
            {
                using (LogEventDataContext logDBContext = new LogEventDataContext(MainPage.ConnectionString))
                {
                    logDBContext.addEvents(saveQueue.ToList().AsReadOnly());
                    saveQueue = new Queue<LogEvent>();
                }
            }
        }

        public void addEvent(string data, string url)
        {
            LogEvent e = new LogEvent();
            e.relativeUrl = url;
            e.sensorEvent = data;
            saveQueue.Enqueue(e);
        }
    }
}
