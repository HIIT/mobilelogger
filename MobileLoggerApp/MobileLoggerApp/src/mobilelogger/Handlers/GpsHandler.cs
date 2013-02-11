using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Device.Location;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MobileLoggerApp.src.mobilelogger.model;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{

    [Obsolete("Need to refactor this class to produce events that are saved into the DB, sending is handled by the MessagingService")]
    public class GpsHandler : AbstractLogHandler
    {
        GeoCoordinateWatcher watcher;
        JObject joCoordinates;
        LogEventDataContext logData;

        public override void SaveSensorLog()
        {
            SaveLogToDB(joCoordinates, "/log/gps");
        }

        public void startCoordinateWatcher()
        {
            if (watcher == null)
            {
                watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                //watcher.MovementThreshold = 20;

                watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
                watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            }
            watcher.Start();
        }

        void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    if (watcher.Permission == GeoPositionPermission.Denied)
                    {
                    }
                    else
                    {
                    }
                    break;

                case GeoPositionStatus.Initializing:
                    break;

                case GeoPositionStatus.NoData:
                    watcher.Stop();
                    break;

                case GeoPositionStatus.Ready:
                    break;
            }
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            joCoordinates.Add("lat", (float)e.Position.Location.Latitude);
            joCoordinates.Add("lon", (float)e.Position.Location.Longitude);
            joCoordinates.Add("alt", (float)e.Position.Location.Altitude);
        }
    }
}
