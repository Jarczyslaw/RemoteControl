using JToolbox.Core.Abstraction;
using JToolbox.WPF.Core;
using System;
using Unity;
using Prism.Unity;
using RemoteControl.Server.Core.Services;
using JToolbox.Desktop.Dialogs;

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
            dialogsService.ShowException(message, exception);
            return true;
        }
    }
}