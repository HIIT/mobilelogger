using Microsoft.Phone.Shell;
using MobileLoggerApp.Handlers;
using System.Collections.Generic;

namespace MobileLoggerApp
{
    class HandlersManager
    {
        private static Dictionary<string, AbstractLogHandler> _logHandlers;
        private static Dictionary<string, bool> handlerState;

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

            if (PhoneApplicationService.Current.State.ContainsKey("Handlers"))
            {
                handlerState = PhoneApplicationService.Current.State["Handlers"] as Dictionary<string, bool>;
            }
            else
                handlerState = new Dictionary<string, bool>();
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

        public void StartEnabledHandlers(bool startHandler)
        {
            AbstractLogHandler logHandler;
            string logHandlerName;

            if (PhoneApplicationService.Current.State.ContainsKey("Handlers"))
            {
                bool isHandlerEnabled;
                handlerState = PhoneApplicationService.Current.State["Handlers"] as Dictionary<string, bool>;

                foreach (KeyValuePair<string, AbstractLogHandler> lh in _logHandlers)
                {
                    logHandler = lh.Value;
                    logHandlerName = lh.Key;

                    handlerState.TryGetValue(logHandlerName, out isHandlerEnabled);

                    if (isHandlerEnabled)
                        logHandler.StartWatcher();
                }
            }
            else
            {
                foreach (KeyValuePair<string, AbstractLogHandler> lh in _logHandlers)
                {
                    logHandler = lh.Value;
                    logHandlerName = lh.Key;

                    if (startHandler)
                        logHandler.StartWatcher();

                    handlerState.Add(logHandlerName, logHandler.IsEnabled);
                }
                PhoneApplicationService.Current.State.Add("Handlers", handlerState);
            }
        }

        public static bool EnableHandler(string handlerName)
        {
            AbstractLogHandler logHandler;
            _logHandlers.TryGetValue(handlerName, out logHandler);

            if (logHandler != null)
            {
                if (!logHandler.IsEnabled)
                {
                    logHandler.StartWatcher();

                    if (handlerState.ContainsKey(handlerName))
                        handlerState[handlerName] = logHandler.IsEnabled;
                    else
                        handlerState.Add(handlerName, logHandler.IsEnabled);
                }
                return logHandler.IsEnabled;
            }

            return false;
        }

        public static void DisableHandler(string handlerName)
        {
            AbstractLogHandler logHandler;
            _logHandlers.TryGetValue(handlerName, out logHandler);

            if (logHandler != null)
            {
                if (logHandler.IsEnabled)
                {
                    if (handlerState.ContainsKey(handlerName))
                        handlerState[handlerName] = false;
                    else
                        handlerState.Add(handlerName, false);

                    logHandler.StopWatcher();
                }
            }
        }

        public static void SaveSensorLog()
        {
            foreach (KeyValuePair<string, AbstractLogHandler> logHandler in _logHandlers)
                logHandler.Value.SaveSensorLog();
        }
    }
}
