using MobileLoggerScheduledAgent.Devicetools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace MobileLoggerScheduledAgent.Database
{
    [Table(Name = "LogEvents")]
    public class LogEvent
    {
        

        private int _eventId;
        private Double _time;
        private String _relativeUrl;

        private String json;
        

        [Column(IsPrimaryKey = true, IsDbGenerated = true)] //, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int EventId
        {
            get
            {
                return _eventId;
            }
            set
            {
                if (_eventId != value)
                {
                    _eventId = value;
                }
            }
        }

        [Column]
        public Double Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (_time == 0.0)
                {
                    _time = value;
                }
            }
        }
        
        [Column]
        public String sensorEvent
        {
            get
            {
                return json;
            }
            set
            {
                if (value != json)
                {
                    json = value;
                }
            }
        }
        [Column(CanBeNull=false)] 
        public String relativeUrl
        {
            get
            {
                return _relativeUrl;
            }
            set
            {
                if (_relativeUrl != value)
                {
                    _relativeUrl = value;
                }
            }
        }

        public override String ToString()
        {
            return "LogEvent:" + EventId; //+"{"+DeviceTools.GetDateTime(_time)+"}";
        }
    }


    public class LogEventDataContext : DataContext
    {
        public const string ConnectionString = @"Data Source = 'isostore:/LogEventDB.sdf';";

        public LogEventDataContext(string connectionString)
            : base(connectionString)
        {
            
        }

        public Table<LogEvent> LogEvents
        {
            get
            {
                return this.GetTable<LogEvent>();
            }
        }

        public void DeleteLogEvent(int id)
    	{
            //LogEventDataContext context;
            using (LogEventDataContext context = new LogEventDataContext(ConnectionString))
        	{
            IQueryable<LogEvent> query =
                        from le in context.LogEvents
                        where le.EventId == id
                        select le;
             
                LogEvent leToDelete = query.FirstOrDefault();
               
                //context.LogEvents.Attach(leToDelete);
                context.LogEvents.DeleteOnSubmit(leToDelete);
                context.SubmitChanges();
        	}
    	}

        public void addEvents(IList<LogEvent> events)
        {
            using (LogEventDataContext context = new LogEventDataContext(ConnectionString))
            {
                foreach (LogEvent e in events)
                {
                    LogEvent le = new LogEvent();
                    e.Time = DeviceTools.GetUnixTime(DateTime.Now);

                    // add the new logEvent to the context
                    context.LogEvents.InsertOnSubmit(e);
                }

                context.SubmitChanges();
            }
        }

        [Obsolete("This method is obsolete and causes bad performance, use addEvents instead")]
        public void addEvent(JObject sensorEvent, String url)
        {
            using (LogEventDataContext context = new LogEventDataContext(ConnectionString))
            {
                // create a new LogEvent instance
                LogEvent le = new LogEvent();
                le.sensorEvent = sensorEvent.ToString();
                le.Time = DeviceTools.GetUnixTime(DateTime.Now);
                le.relativeUrl = url;

                // add the new logEvent to the context
                context.LogEvents.InsertOnSubmit(le);
                try
                {
                    // save changes to the database
                    context.SubmitChanges();
                } catch (System.SystemException e) {
                    System.Diagnostics.Debug.WriteLine("SQLException"+e);
                }
            }
        }

        public List<LogEvent> GetLogEvents()
        {
            List<LogEvent> logEventList = null;
            using (LogEventDataContext context = new LogEventDataContext(ConnectionString))
            {
                IQueryable<LogEvent> query =
                    from le in context.LogEvents
                    select le;
                try
                {
                    logEventList = query.ToList();
                }
                catch (Exception e) {
                    System.Diagnostics.Debug.WriteLine("At LogDatabase: "+e.Message);
                }
            }
            return logEventList;
        }

        public LogEvent entity { get; set; }


    }
}