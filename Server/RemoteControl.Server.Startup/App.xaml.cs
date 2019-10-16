using JToolbox.AppConfig;
using JToolbox.Core.Abstraction;
using JToolbox.Desktop.Dialogs;
using JToolbox.Logging;
using JToolbox.WPF.PrismCore;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using RemoteControl.Server.Core.Views;
using System.Windows;

namespace RemoteControl.Server.Startup
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILoggerService, LoggerService>();
            containerRegistry.RegisterSingleton<IDialogsService, DialogsService>();
            containerRegistry.RegisterSingleton<IAppConfigService, AppConfigService>();
            RegisterGlobalExceptionHandler();
        }

        private void RegisterGlobalExceptionHandler()
        {
            var globalExceptionHandler = Container.Resolve<AppExceptionHandler>();
            globalExceptionHandler.Register();
        }

        protected override void InitializeShell(Window shell)
        {
            Current.MainWindow = shell;
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            var resolver = new ViewModelResolver();
            var currentAssembly = typeof(ShellWindow).Assembly;
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType => resolver.Resolve(viewType, currentAssembly));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var logger = Container.Resolve<ILoggerService>();
            logger.Info("App started");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var logger = Container.Resolve<ILoggerService>();
            logger.Info("App finished");
        }
    }
}