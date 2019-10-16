using System;
using System.Threading.Tasks;

namespace JToolbox.XamarinForms.Core
{
    public abstract class GlobalExceptionHandler
    {
        public void Attach()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        }

        public void Detach()
        {
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException -= TaskSchedulerOnUnobservedTaskException;
        }

        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            HandleException(nameof(TaskSchedulerOnUnobservedTaskException), unobservedTaskExceptionEventArgs.Exception);
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            HandleException(nameof(CurrentDomainOnUnhandledException), unhandledExceptionEventArgs.ExceptionObject as Exception);
        }

        protected abstract void HandleException(string exceptionSource, Exception exception);
    }
}