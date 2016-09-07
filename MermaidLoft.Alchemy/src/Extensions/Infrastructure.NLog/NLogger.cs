using System;
using NLog;

namespace Infrastructure.Log4Net
{
    /// <summary>NLog based logger implementation.
    /// </summary>
    public class NLogger : Logging.ILogger
    {
        private readonly Logger _logger;

        /// <summary>Parameterized constructor.
        /// </summary>
        /// <param name="log"></param>
        public NLogger(Logger logger)
        {
            _logger = logger;
        }

        #region ILogger Members

        public bool IsDebugEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }
        public void Debug(object message)
        {
            _logger.Debug(message);
        }
        public void DebugFormat(string format, params object[] args)
        {
            _logger.Debug(format, args);
        }
        public void Debug(object message, Exception exception)
        {
            _logger.Debug(exception, message != null ? message.ToString() : "");
        }
        public void Info(object message)
        {
            _logger.Info(message);
        }
        public void InfoFormat(string format, params object[] args)
        {
            _logger.Info(format, args);
        }
        public void Info(object message, Exception exception)
        {
            _logger.Info(exception, message != null ? message.ToString() : "");
        }
        public void Error(object message)
        {
            _logger.Error(message);
        }
        public void ErrorFormat(string format, params object[] args)
        {
            _logger.Error(format, args);
        }
        public void Error(object message, Exception exception)
        {
            _logger.Error(exception, message != null ? message.ToString() : "");
        }
        public void Warn(object message)
        {
            _logger.Warn(message);
        }
        public void WarnFormat(string format, params object[] args)
        {
            _logger.Warn(format, args);
        }
        public void Warn(object message, Exception exception)
        {
            _logger.Warn(exception, message != null ? message.ToString() : "");
        }
        public void Fatal(object message)
        {
            _logger.Fatal(message);
        }
        public void FatalFormat(string format, params object[] args)
        {
            _logger.Fatal(format, args);
        }
        public void Fatal(object message, Exception exception)
        {
            _logger.Fatal(exception, message != null ? message.ToString() : "");
        }

        #endregion
    }
}