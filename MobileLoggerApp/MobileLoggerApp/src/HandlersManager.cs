using MobileLoggerApp.Handlers;
using System.Collections.Generic;

namespace MobileLoggerApp
{
    class HandlersManager
    {
        private static Dictionary<string, AbstractLogHandler> _logHandlers;

        public static Dictionary<string, AbstractLogHandler> LogHandlers
        {
            get { return _logHandlers; }
            set
            {
                if (value != _logHandlers)
                {
                    _logHandlers = value;
                }
            }
        }

        public HandlersManager()
        {
            _logHandlers = new Dictionary<string, AbstractLogHandler>();
        }

        public void InitHandlers()
        {
            AccelHandler accelerometer = new AccelHandler();
            _logHandlers.Add("Accelerometer", accelerometer);

            CompassHandler compass = new CompassHandler();
            _logHandlers.Add("Compass", compass);

            GpsHandler gps = new GpsHandler();
            _logHandlers.Add("GPS", gps);

            GyroHandler gyroscope = new GyroHandler();
            _logHandlers.Add("Gyroscope", gyroscope);

            KeyboardHandler keyboard = new KeyboardHandler();
            _logHandlers.Add("Keyboard", keyboard);

            KeyPressHandler keyPress = new KeyPressHandler();
            _logHandlers.Add("Key Press", keyPress);

            NetworkHandler network = new NetworkHandler();
            _logHandlers.Add("Network", network);

            ScreenTouchHandler screenTouch = new ScreenTouchHandler();
            _logHandlers.Add("Screen Touch", screenTouch);

            SearchDataHandler searchData = new SearchDataHandler();
            _logHandlers.Add("Search Data", searchData);

            WeatherDataHandler weatherData = new WeatherDataHandler();
            _logHandlers.Add("Weather", weatherData);
        }

        public void StartEnabledHandlers()
        {
            AbstractLogHandler logHandler;
            string logHandlerName;

            foreach (KeyValuePair<string, AbstractLogHandler> lh in _logHandlers)
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
            //AbstractLogHandler logHandler;
            //_logHandlers.TryGetValue(handlerName, out logHandler);

            //if (logHandler != null)
            //{
            //    if (!logHandler.IsEnabled)
            //    {
            //        MainPage.appSettings[handlerName] = true;
            //        logHandler.StartWatcher();
            //    }
            //}
        }

        public static void DisableHandler(string handlerName)
        {
            //AbstractLogHandler logHandler;
            //_logHandlers.TryGetValue(handlerName, out logHandler);

            //if (logHandler != null)
            //{
            //    if (logHandler.IsEnabled)
            //    {
            //        MainPage.appSettings[handlerName] = false;
            //        logHandler.StopWatcher();
            //    }
            //}
        }

        public static void SaveSensorLog()
        {
            foreach (KeyValuePair<string, AbstractLogHandler> logHandler in _logHandlers)
                logHandler.Value.SaveSensorLog();
        }
    }
}
