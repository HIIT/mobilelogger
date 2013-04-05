using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileLoggerApp
{
    interface HttpRequestable
    {
        void Callback(string data);

        void HandleRequestError(Exception exception);
    }
}
