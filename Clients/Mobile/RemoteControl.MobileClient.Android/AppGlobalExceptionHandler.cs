using JToolbox.Core.Abstraction;
using JToolbox.XamarinForms.Core;
using RemoteControl.MobileClient.Core;
using System;
using Prism.Ioc;

namespace RemoteControl.MobileClient.Droid
{
    public class AppGlobalExceptionHandler : GlobalExceptionHandler
    {
        protected override void HandleException(string exceptionSource, Exception exception)
        {
            var logger = App.ContainerProvider.Resolve<ILoggerService>();
            logger.Fatal(exceptionSource, exception);
        }
    }
}