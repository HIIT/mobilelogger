using System;

namespace MobileLogger
{
    /// <summary>
    /// This class contains application state related variables.
    /// </summary>
    public static class StateUtilities
    {
        private static Boolean startHandlers;
        public static Boolean StartHandlers
        {
            get { return startHandlers; }
            set { startHandlers = value; }
        }

        private static Boolean newSearch;
        public static Boolean NewSearch
        {
            get { return newSearch; }
            set { newSearch = value; }
        }
    }
}
