using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Device.Location;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{

    [Obsolete("Need to refactor this class to produce events that are saved into the DB, sending is handled by the MessagingService")]
    public class GpsHandler : AbstractLogHandler
    {
        GeoCoordinateWatcher watcher;
        Message gpsData;
        JObject joCoordinates;
        Dictionary<string, float> coordinates = new Dictionary<string, float>();

        public override Boolean SendData() {

            joCoordinates = new JObject();
            joCoordinates.Add(coordinates);

            gpsData = new Message(ServerLocations.serverRoot + "/log/gps", joCoordinates, "put");
            gpsData.SendMessage();

            return false;
        }

        public override void HandleSendError() { }

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
            if (coordinates.ContainsKey("lat"))
            {
                coordinates["lat"] = (float)e.Position.Location.Latitude;
            }
            else
            {
                coordinates.Add("lat", (float)e.Position.Location.Latitude);
            }
            if (coordinates.ContainsKey("lon"))
            {
                coordinates["lon"] = (float)e.Position.Location.Longitude;
            }
            else
            {
            coordinates.Add("lon", (float)e.Position.Location.Longitude);
            }
            if (coordinates.ContainsKey("alt"))
            {
                coordinates["alt"] = (float)e.Position.Location.Altitude;
            }
            else
            {
                coordinates.Add("alt", (float)e.Position.Location.Altitude);
            }
        }

        public override string ToString()
        {
            return string.Format("Latitude: {0}, Longitude: {1}, Altitude: {2}", coordinates["lat"], coordinates["lon"], coordinates["alt"]);
        }
    }
}
