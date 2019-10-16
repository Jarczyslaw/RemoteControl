using JToolbox.Core.Abstraction;
using JToolbox.Desktop.Dialogs;
using JToolbox.WPF.Core;
using System;

namespace RemoteControl.Server.Startup
{
    public class AppExceptionHandler : GlobalExceptionHandler
    {
        private readonly IDialogsService dialogsService;
        private readonly ILoggerService loggerService;

        public AppExceptionHandler(IDialogsService dialogsService, ILoggerService loggerService)
        {
            this.dialogsService = dialogsService;
            this.loggerService = loggerService;
        }

        public override bool HandleException(string source, Exception exception)
        {
            var message = $"Unexpected critical exception - {source}";
            loggerService.Fatal(exception, message);
            dialogsService.ShowCriticalException(message, exception);
            return true;
        }
    }
}