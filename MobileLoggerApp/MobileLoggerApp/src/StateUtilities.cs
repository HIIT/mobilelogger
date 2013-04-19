using System;

namespace MobileLogger
{
    public static class StateUtilities
    {
        private static Boolean isLaunching;

        public static Boolean IsLaunching
        {
            get { return isLaunching; }
            set { isLaunching = value; }
        }
    }
}
