using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;



namespace MobileLoggerApp.src.mobilelogger.model
{
    
    [Table(Name = "LogEvents")]
    public class LogEvent
    {
        private int _eventId;
        private Double _time;
        private String _relativeUrl;

        private String json = "123";
        

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
                if (value.ToString() != json)
                {
                    json = value.ToString();
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
            return "LogEvent:" + EventId +"{"+DeviceTools.GetDateTime(_time)+"}";
        }
    }


    public class LogEventDataContext : DataContext
    {
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

        public void addEvent(String sensorEvent, String url)
        {
            using (LogEventDataContext context = new LogEventDataContext(MobileLoggerApp.MainPage.ConnectionString))
            {
                // create a new LogEvent instance
                LogEvent le = new LogEvent();
                le.sensorEvent = sensorEvent;
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

        public IList<LogEvent> GetLogEvents()
        {
            IList<LogEvent> logEventList = null;
            using (LogEventDataContext context = new LogEventDataContext(MobileLoggerApp.MainPage.ConnectionString))
            {
                IQueryable<LogEvent> query =
                    from le in context.LogEvents
                    select le;
                try
                {
                    logEventList = query.ToList();
                }
                catch (Exception e) { }
            }

            return logEventList;
        }
    }

}