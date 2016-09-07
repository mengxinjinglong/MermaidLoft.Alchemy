using System;
using Infrastructure.Logging;

namespace Infrastructure.Log4Net
{
    /// <summary>Log4Net based logger factory.
    /// </summary>
    public class NLoggerFactory : ILoggerFactory
    {
        /// <summary>Parameterized constructor.
        /// </summary>
        /// <param name="configFile"></param>
        public NLoggerFactory(string configFile)
        {
            //var file = new FileInfo(configFile);
            //if (!file.Exists)
            //{
            //    file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile));
            //}

            //if (file.Exists)
            //{
            //    XmlConfigurator.ConfigureAndWatch(file);
            //}
            //else
            //{
            //    BasicConfigurator.Configure(new ConsoleAppender { Layout = new PatternLayout() });
            //}
        }
        /// <summary>Create a new Log4NetLogger instance.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILogger Create(string name)
        {
            return new NLogger(NLog.LogManager.GetLogger(name));
        }
        /// <summary>Create a new Log4NetLogger instance.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ILogger Create(Type type)
        {
            return new NLogger(NLog.LogManager.GetLogger("Default",type)); 
        }
    }
}
