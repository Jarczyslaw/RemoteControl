using Prism.Ioc;
using Prism.Unity;
using System;
using System.Windows;

namespace RemoteControl.Server.Startup
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            throw new NotImplementedException();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            throw new NotImplementedException();
        }

        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}