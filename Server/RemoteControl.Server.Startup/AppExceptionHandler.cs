using JToolbox.Core.Abstraction;
using JToolbox.WPF.Core;
using System;

namespace RemoteControl.Server.Startup
{
    public class AppExceptionHandler : GlobalExceptionHandler
    {
        private readonly ILoggerService loggerService;

        public AppExceptionHandler(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        public override bool HandleException(string source, Exception exception)
        {
            var message = $"Unexpected critical exception - {source}";
            loggerService.Fatal(exception, message);
            return true;
        }
    }
}