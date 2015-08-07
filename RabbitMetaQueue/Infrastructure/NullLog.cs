using System;

namespace RabbitMetaQueue.Infrastructure
{
    /* Derived from BaseLog instead of ILog so String.Format does get applied in the unit tests */
    public class NullLog : BaseLog
    {
        protected override void Log(LogLevel level, string message, Exception exception)
        {
        }
    }
}
