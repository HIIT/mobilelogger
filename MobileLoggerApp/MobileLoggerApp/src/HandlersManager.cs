﻿using MobileLoggerApp.Handlers;
using System.Collections.Generic;

namespace MobileLoggerApp
{
    class HandlersManager
    {
       public static Dictionary<string, AbstractLogHandler> logHandlers;

        public HandlersManager()
        {
            logHandlers = new Dictionary<string, AbstractLogHandler>();
        }

        public void InitHandlers()
        {
            AccelHandler accelerometer = new AccelHandler();
            logHandlers.Add("Accelerometer", accelerometer);

            CompassHandler compass = new CompassHandler();
            logHandlers.Add("Compass", compass);

            GpsHandler gps = new GpsHandler();
            logHandlers.Add("GPS", gps);

            GyroHandler gyroscope = new GyroHandler();
            logHandlers.Add("Gyroscope", gyroscope);

            KeyboardHandler keyboard = new KeyboardHandler();
            logHandlers.Add("Keyboard", keyboard);

            KeyPressHandler keyPress = new KeyPressHandler();
            logHandlers.Add("Key Press", keyPress);

            NetworkHandler network = new NetworkHandler();
            logHandlers.Add("Network", network);

            ScreenTouchHandler screenTouch = new ScreenTouchHandler();
            logHandlers.Add("Screen Touch", screenTouch);

            SearchDataHandler searchData = new SearchDataHandler();
            logHandlers.Add("Search Data", searchData);

            WeatherDataHandler weatherData = new WeatherDataHandler();
            logHandlers.Add("Weather", weatherData);
        }

        public void StartEnabledHandlers()
        {
            AbstractLogHandler logHandler;
            string logHandlerName;

            foreach (KeyValuePair<string, AbstractLogHandler> lh in logHandlers)
            {
                logHandler = lh.Value;
                logHandlerName = lh.Key;

                if (GetIsLogHandlerEnabled(logHandlerName))
                    logHandler.StartWatcher();
            }
        }

        private bool GetIsLogHandlerEnabled(string logHandlerName)
        {
            bool isLogHandlerEnabled = true;

            if (!MainPage.appSettings.Contains(logHandlerName))
            {
                MainPage.appSettings.Add(logHandlerName, isLogHandlerEnabled);
            }
            else
            {
                MainPage.appSettings.TryGetValue(logHandlerName, out isLogHandlerEnabled);
            }
            return isLogHandlerEnabled;
        }

        public static void EnableHandler(string handlerName)
        {
            AbstractLogHandler logHandler;
            logHandlers.TryGetValue(handlerName, out logHandler);

            if (logHandler != null)
            {
                if (!logHandler.IsEnabled)
                {
                    MainPage.appSettings[handlerName] = true;
                    logHandler.StartWatcher();
                }
            }
        }

        public static void DisableHandler(string handlerName)
        {
            AbstractLogHandler logHandler;
            logHandlers.TryGetValue(handlerName, out logHandler);

            if (logHandler != null)
            {
                if (logHandler.IsEnabled)
                {
                    MainPage.appSettings[handlerName] = false;
                    logHandler.StopWatcher();
                }
            }
        }

        public static void SaveSensorLog()
        {
            foreach (KeyValuePair<string, AbstractLogHandler> logHandler in logHandlers)
                logHandler.Value.SaveSensorLog();
        }
    }
}