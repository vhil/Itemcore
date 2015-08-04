using System;
using System.Collections.Generic;
using System.Linq;

namespace Itemcore.Logging
{
    /// <summary>
    /// The Logging Manager
    /// </summary>
    public class LoggingService : ILoggingService
    {
        private readonly ILoggingProvider[] _loggingProviders;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingService"/> class.
        /// </summary>
        /// <param name="providers">The providers.</param>
        public LoggingService(IEnumerable<ILoggingProvider> providers)
        {
            _loggingProviders = providers.ToArray();
        }

        /// <summary>
        /// Logs the specified level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="formatParams">The format parameters.</param>
        public void Log(SeverityLevel level, string message, object owner, Exception exception = null, params object[] formatParams)
        {
            foreach (var provider in _loggingProviders)
            {
                provider.Log(level, message, owner, exception, formatParams);
            }
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="formatParams">The format parameters.</param>
        public void Info(string message, object owner, params object[] formatParams)
        {
            foreach (var provider in _loggingProviders)
            {
                provider.Log(SeverityLevel.Info, message, owner, null, formatParams);
            }
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="formatParams">The format parameters.</param>
        public void Warn(string message, object owner, Exception exception = null, params object[] formatParams)
        {
            foreach (var provider in _loggingProviders)
            {
                provider.Log(SeverityLevel.Warn, message, owner, exception, formatParams);
            }
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="formatParams">The format parameters.</param>
        public void Debug(string message, object owner, params object[] formatParams)
        {
            foreach (var provider in _loggingProviders)
            {
                provider.Log(SeverityLevel.Debug, message, owner, null, formatParams);
            }
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="formatParams">The format parameters.</param>
        public void Error(string message, object owner, Exception exception = null, params object[] formatParams)
        {
            foreach (var provider in _loggingProviders)
            {
                provider.Log(SeverityLevel.Error, message, owner, exception, formatParams);
            }
        }

        /// <summary>
        /// Audits the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="formatParams">The format parameters.</param>
        public void Audit(string message, object owner, params object[] formatParams)
        {
            foreach (var provider in _loggingProviders)
            {
                provider.Log(SeverityLevel.Audit, message, owner, null, formatParams);
            }
        }
    }
}
