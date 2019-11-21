using JToolbox.Core.Abstraction;
using System;

namespace RemoteControl.Server.Core.Services
{
    public interface ILogsAggregator
    {
        event OnMessage OnMessage;

        ILoggerService LoggerService { get; }

        void Error(Exception exception);

        void Info(string message);
    }
}