﻿using MobileLoggerScheduledAgent.Devicetools;
using System;
using System.Device.Location;

namespace MobileLoggerApp.Handlers
{
    public class GpsHandler : AbstractLogHandler
    {
        //used in WeatherInformation
        public static GeoCoordinateWatcher coordinateWatcher;
        public static string latitude, longitude, altitude;

        public GpsHandler()
        {
            coordinateWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default); //using high might slow down the app
        }

        public override void SaveSensorLog()
        {
            if (coordinateWatcher.Status == GeoPositionStatus.Ready)
                SaveLogToDB(this.data, "/log/gps");
        }

        public override void StartWatcher()
        {
            string coordWatcherPermission = coordinateWatcher.Permission.ToString();
            string gpsPermissionDenied = GeoPositionPermission.Denied.ToString();

            if (!coordWatcherPermission.Equals(gpsPermissionDenied))
            {
                coordinateWatcher.MovementThreshold = 20;

                coordinateWatcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(coordinate_StatusChanged);
                coordinateWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(coordinate_PositionChanged);
                coordinateWatcher.Start();
                this.IsEnabled = true;
            }
        }

        public override void StopWatcher()
        {
            coordinateWatcher.Stop();
            this.IsEnabled = false;
        }

        private void coordinate_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    if (coordinateWatcher.Permission == GeoPositionPermission.Denied)
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

        private void coordinate_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (!e.Position.Location.IsUnknown)
            {
                latitude = e.Position.Location.Latitude.ToString();
                longitude = e.Position.Location.Longitude.ToString();
                altitude = e.Position.Location.Altitude.ToString();

                AddJOValue("lat", latitude.Replace(",", "."));
                AddJOValue("lon", longitude.Replace(",", "."));
                AddJOValue("alt", altitude.Replace(",", "."));
                AddJOValue("timestamp", UnixTime(e.Position.Timestamp) );
            }
        }
    }
}
