using System;
using Infrastructure.Logging;

namespace Infrastructure.SeriLog
{
    public class SeriLoggerFactory : ILoggerFactory
    {
        public ILogger Create(Type type)
        {
            return new SeriLogger(Serilog.Log.Logger);
        }

        public ILogger Create(string name)
        {
            return new SeriLogger(Serilog.Log.Logger);
        }
    }
}
