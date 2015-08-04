using System;

namespace Itemcore.Logging
{
    public interface ILoggingProvider
    {
        void Log(SeverityLevel level, string message, object owner, Exception exception = null, params object[] formatParams);
    }
}