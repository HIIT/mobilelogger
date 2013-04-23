using System;

namespace MobileLogger
{
    public static class StateUtilities
    {
        private static Boolean isFirstRun;
        public static Boolean IsFirstRun
        {
            get { return isFirstRun; }
            set { isFirstRun = value; }
        }

        private static Boolean startHandlers;
        public static Boolean StartHandlers
        {
            get { return startHandlers; }
            set { startHandlers = value; }
        }
    }
}
