using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Device.Location;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class GpsHandler : AbstractLogHandler
    {
        GeoCoordinateWatcher watcher;
        JObject joCoordinates;

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

            if (joCoordinates == null)
            {
                joCoordinates = new JObject();
            }
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
                    break;

                case GeoPositionStatus.Ready:
                    break;
            }
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (joCoordinates["lat"] == null)
            {
                joCoordinates.Add("lat", (float)e.Position.Location.Latitude);
            }
            else
            {
                joCoordinates["lat"].Replace((float)e.Position.Location.Latitude);
            }

            if (joCoordinates["lon"] == null)
            {
                joCoordinates.Add("lon", (float)e.Position.Location.Longitude);
            }
            else
            {
                joCoordinates["lon"].Replace((float)e.Position.Location.Longitude);
            }

            if (joCoordinates["alt"] == null)
            {
                joCoordinates.Add("alt", (float)e.Position.Location.Altitude);
            }
            else
            {
                joCoordinates["alt"].Replace((float)e.Position.Location.Altitude);
            }
        }
    }
}
