using System;

namespace RabbitMetaQueue.Infrastructure
{
    public interface ILog
    {
        bool IsDebugEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }

        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);

        void Debug(string message, Exception exception);
        void Info(string message, Exception exception);
        void Warn(string message, Exception exception);
        void Error(string message, Exception exception);
        void Fatal(string message, Exception exception);

        void DebugFormat(string format, params object[] args);
        void InfoFormat(string format, params object[] args);
        void WarnFormat(string format, params object[] args);
        void ErrorFormat(string format, params object[] args);
        void FatalFormat(string format, params object[] args);

        void DebugFormat(IFormatProvider provider, string format, params object[] args);
        void InfoFormat(IFormatProvider provider, string format, params object[] args);
        void WarnFormat(IFormatProvider provider, string format, params object[] args);
        void ErrorFormat(IFormatProvider provider, string format, params object[] args);
        void FatalFormat(IFormatProvider provider, string format, params object[] args);
    }
}
