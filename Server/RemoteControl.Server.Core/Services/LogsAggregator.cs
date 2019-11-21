using JToolbox.Core.Abstraction;
using System;

namespace RemoteControl.Server.Core.Services
{
    public delegate void OnMessage(string message);

    public class LogsAggregator : ILogsAggregator
    {
        public event OnMessage OnMessage = delegate { };

        public LogsAggregator(ILoggerService loggerService)
        {
            LoggerService = loggerService;
        }

        public ILoggerService LoggerService { get; }

        public void Info(string message)
        {
            LoggerService.Info(message);
            OnMessage(message);
        }

        public void Error(Exception exception)
        {
            LoggerService.Error(exception);
            OnMessage("Exception occured: " + exception.Message);
        }
    }
}