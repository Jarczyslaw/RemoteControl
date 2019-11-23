using JToolbox.Core.Abstraction;
using System;

namespace RemoteControl.Server.Messages
{
    public interface IMessagesAggregator
    {
        event OnMessage OnMessage;

        event OnStatusMessage OnStatusMessage;

        ILoggerService LoggerService { get; }

        void Error(Exception exception);

        void Info(string message);

        void StatusMessage(string message);
    }
}