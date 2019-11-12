using JToolbox.Core.Abstraction;
using JToolbox.WPF.Core;
using System;
using Unity;
using Prism.Unity;
using RemoteControl.Server.Core.Services;

namespace RemoteControl.Server.Startup
{
    public class AppExceptionHandler : GlobalExceptionHandler
    {
        private readonly IUnityContainer container;

        public AppExceptionHandler(IUnityContainer container)
        {
            this.container = container;
        }

        public override bool HandleException(string source, Exception exception)
        {
            var message = $"Unexpected critical exception - {source}";
            container.Resolve<ILoggerService>().Fatal(exception, message);
            container.TryResolve<IDialogsService>()?.ShowException(exception);
            return true;
        }
    }
}