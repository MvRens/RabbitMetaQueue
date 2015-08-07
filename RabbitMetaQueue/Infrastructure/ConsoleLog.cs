using System;
using RabbitMetaQueue.Resources;

namespace RabbitMetaQueue.Infrastructure
{
    public class ConsoleLog : BaseLog
    {
        protected override void Log(LogLevel level, string message, Exception exception)
        {
            Console.Write(Strings.LogPrefix);

            switch(level)
            {
                case LogLevel.Warn: Console.Write(Strings.LogWarning); break;
                case LogLevel.Error: Console.Write(Strings.LogError); break;
                case LogLevel.Fatal: Console.Write(Strings.LogFatal); break;
            }

            Console.WriteLine(message);

            if (exception != null)
                Console.WriteLine(exception.Message);
        }
    }
}
