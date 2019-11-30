using JToolbox.Core.Abstraction;
using JToolbox.Desktop.Dialogs;
using JToolbox.WPF.Core;
using System;

namespace RemoteControl.Server.Startup
{
    public class AppExceptionHandler : GlobalExceptionHandler
    {
        private readonly ILoggerService loggerService;
        private readonly IDialogsService dialogsService;

        public AppExceptionHandler(ILoggerService loggerService, IDialogsService dialogsService)
        {
            this.loggerService = loggerService;
            this.dialogsService = dialogsService;
        }

        public override bool HandleException(string source, Exception exception)
        {
            var message = $"Unexpected critical exception - {source}";
            loggerService.Fatal(exception, message);
            dialogsService.ShowException(exception, message);
            return true;
        }
    }
}