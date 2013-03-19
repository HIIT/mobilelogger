using Newtonsoft.Json.Linq;
using System;
using System.Device.Location;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class GpsHandler : AbstractLogHandler
    {
        GeoCoordinateWatcher coordinateWatcher;
        public static JObject joCoordinates;

        public override void SaveSensorLog()
        {
            SaveLogToDB(joCoordinates, "/log/gps");
        }

        public void StartCoordinateWatcher()
        {
            if (coordinateWatcher == null)
            {
                coordinateWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                coordinateWatcher.MovementThreshold = 20;

                coordinateWatcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(coordinate_StatusChanged);
                coordinateWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(coordinate_PositionChanged);
            }
            coordinateWatcher.Start();

            if (joCoordinates == null)
                joCoordinates = new JObject();
        }

        void coordinate_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    break;

                case GeoPositionStatus.Initializing:
                    break;

                case GeoPositionStatus.NoData:
                    break;

                case GeoPositionStatus.Ready:
                    break;
            }
        }

        void coordinate_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            AddJOValue("lat", e.Position.Location.Latitude);
            AddJOValue("lon", e.Position.Location.Longitude);
            AddJOValue("alt", e.Position.Location.Altitude);
        }

        private void AddJOValue(String key, double value)
        {
            if (joCoordinates[key] == null)
                joCoordinates.Add(key, (float)value);
            else
                joCoordinates[key].Replace((float)value);
        }
    }
}
