using JToolbox.Core.Abstraction;
using System;

namespace RemoteControl.Server.Core.Services
{
    public delegate void OnMessage(string message);

    public delegate void OnStatusMessage(string message);

    public class MessagesAggregator : IMessagesAggregator
    {
        public event OnMessage OnMessage = delegate { };

        public event OnStatusMessage OnStatusMessage = delegate { };

        public MessagesAggregator(ILoggerService loggerService)
        {
            LoggerService = loggerService;
        }

        public ILoggerService LoggerService { get; }

        public void Info(string message)
        {
            LoggerService.Info(message);
            OnMessage(message);
        }

        public void StatusMessage(string message)
        {
            OnStatusMessage(message);
        }

        public void Error(Exception exception)
        {
            LoggerService.Error(exception);
            OnMessage("Exception occured: " + exception.Message);
        }
    }
}