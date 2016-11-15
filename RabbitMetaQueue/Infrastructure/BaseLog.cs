using System;

namespace RabbitMetaQueue.Infrastructure
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }


    public abstract class BaseLog : ILog
    {
        public bool IsDebugEnabled { get; set; }
        public bool IsInfoEnabled { get; set; }
        public bool IsWarnEnabled { get; set; }
        public bool IsErrorEnabled { get; set; }
        public bool IsFatalEnabled { get; set; }


        protected BaseLog()
        {
            IsDebugEnabled = false;
            IsInfoEnabled = true;
            IsWarnEnabled = true;
            IsErrorEnabled = true;
            IsFatalEnabled = true;
        }


        protected abstract void Log(LogLevel level, string message, Exception exception);


        protected void LogFormat(LogLevel level, string format, params object[] args)
        {
            Log(level, string.Format(format, args), null);
        }

        protected void LogFormat(LogLevel level, IFormatProvider provider, string format, params object[] args)
        {
            Log(level, string.Format(provider, format, args), null);
        }


        public void Debug(string message)
        {
            if (IsDebugEnabled)
                Log(LogLevel.Debug, message, null);
        }

        public void Info(string message)
        {
            if (IsInfoEnabled)
                Log(LogLevel.Info, message, null);
        }

        public void Warn(string message)
        {
            if (IsWarnEnabled)
                Log(LogLevel.Warn, message, null);
        }

        public void Error(string message)
        {
            if (IsErrorEnabled)
                Log(LogLevel.Error, message, null);
        }

        public void Fatal(string message)
        {
            if (IsFatalEnabled)
                Log(LogLevel.Fatal, message, null);
        }

        public void Debug(string message, Exception exception)
        {
            if (IsDebugEnabled)
                Log(LogLevel.Debug, message, exception);
        }

        public void Info(string message, Exception exception)
        {
            if (IsInfoEnabled)
                Log(LogLevel.Info, message, exception);
        }

        public void Warn(string message, Exception exception)
        {
            if (IsWarnEnabled)
                Log(LogLevel.Warn, message, exception);
        }

        public void Error(string message, Exception exception)
        {
            if (IsErrorEnabled)
                Log(LogLevel.Error, message, exception);
        }

        public void Fatal(string message, Exception exception)
        {
            if (IsFatalEnabled)
                Log(LogLevel.Fatal, message, exception);
        }

        public void DebugFormat(string format, params object[] args)
        {
            if (IsDebugEnabled)
                LogFormat(LogLevel.Debug, format, args);
        }

        public void InfoFormat(string format, params object[] args)
        {
            if (IsInfoEnabled)
                LogFormat(LogLevel.Info, format, args);
        }

        public void WarnFormat(string format, params object[] args)
        {
            if (IsWarnEnabled)
                LogFormat(LogLevel.Warn, format, args);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            if (IsErrorEnabled)
                LogFormat(LogLevel.Error, format, args);
        }

        public void FatalFormat(string format, params object[] args)
        {
            if (IsFatalEnabled)
                LogFormat(LogLevel.Fatal, format, args);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (IsDebugEnabled)
                LogFormat(LogLevel.Debug, provider, format, args);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (IsInfoEnabled)
                LogFormat(LogLevel.Info, provider, format, args);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (IsWarnEnabled)
                LogFormat(LogLevel.Warn, provider, format, args);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (IsErrorEnabled)
                LogFormat(LogLevel.Error, provider, format, args);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (IsFatalEnabled)
                LogFormat(LogLevel.Fatal, provider, format, args);
        }
    }
}
