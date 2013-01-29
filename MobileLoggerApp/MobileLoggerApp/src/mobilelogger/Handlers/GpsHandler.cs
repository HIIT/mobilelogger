using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileLoggerApp.src.mobilelogger.Handlers
{
    public class GpsHandler : AbstractLogHandler
    {
        public override Boolean SendData() {
            return false;
        }

        public override void HandleSendError() { }
    }
}
