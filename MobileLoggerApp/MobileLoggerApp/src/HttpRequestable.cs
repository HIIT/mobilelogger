using System;

namespace MobileLoggerApp
{
    interface HttpRequestable
    {
        void Callback(string data);

        void HandleRequestError(Exception exception);
    }
}
