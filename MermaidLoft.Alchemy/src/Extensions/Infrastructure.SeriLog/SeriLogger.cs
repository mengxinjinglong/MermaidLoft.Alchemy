using System;
using Infrastructure.Logging;

namespace Infrastructure.SeriLog
{
    public class SeriLogger : ILogger
    {
        private readonly Serilog.ILogger _logger;

        /// <summary>Parameterized constructor.
        /// </summary>
        /// <param name="log"></param>
        public SeriLogger(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        #region ILogger Members

        public bool IsDebugEnabled
        {
            get { return _logger.IsEnabled(Serilog.Events.LogEventLevel.Debug); }
        }
        public void Debug(object message)
        {
            _logger.Debug(message.ToString());
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
            _logger.Information(message.ToString());
        }
        public void InfoFormat(string format, params object[] args)
        {
            _logger.Information(format, args);
        }
        public void Info(object message, Exception exception)
        {
            _logger.Information(exception, message != null ? message.ToString() : "");
        }
        public void Error(object message)
        {
            _logger.Error(message.ToString());
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
            _logger.Warning(message.ToString());
        }
        public void WarnFormat(string format, params object[] args)
        {
            _logger.Warning(format, args);
        }
        public void Warn(object message, Exception exception)
        {
            _logger.Warning(exception, message != null ? message.ToString() : "");
        }
        public void Fatal(object message)
        {
            _logger.Fatal(message.ToString());
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
