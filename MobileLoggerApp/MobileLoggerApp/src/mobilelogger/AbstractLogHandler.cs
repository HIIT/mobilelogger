using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileLoggerApp.src.mobilelogger
{
    public abstract class AbstractLogHandler
    {

        public abstract Boolean SendData();


        public abstract void HandleSendError();

    }
}
