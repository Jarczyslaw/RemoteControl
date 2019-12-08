using JToolbox.AppConfig;
using JToolbox.Core.Abstraction;
using JToolbox.Core.Utilities;
using JToolbox.Desktop.Core.Services;
using JToolbox.Desktop.Dialogs;
using JToolbox.Logging;
using JToolbox.WPF.PrismCore;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using RemoteControl.Server.AppSettings;
using RemoteControl.Server.Connections;
using RemoteControl.Server.Core.Services;
using RemoteControl.Server.Core.Views;
using RemoteControl.Server.Messages;
using RemoteControl.Server.RemoteCommands;
using System;
using System.Windows;
using Unity;

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
            containerRegistry.RegisterSingleton<IShellDialogsService, ShellDialogsService>();
            containerRegistry.RegisterSingleton<IAppConfigService, AppConfigService>();
            containerRegistry.RegisterSingleton<ISystemService, SystemService>();
            containerRegistry.RegisterSingleton<IRemoteCommandsService, RemoteCommandsService>();
            containerRegistry.RegisterSingleton<IMessagesAggregator, MessagesAggregator>();
            containerRegistry.RegisterSingleton<IConnectionsService, ConnectionsService>();
            containerRegistry.RegisterSingleton<ISettingsService, SettingsService>();
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

            InitializeSettings();
        }

        private void InitializeSettings()
        {
            var logger = Container.Resolve<ILoggerService>();
            var settingsService = Container.Resolve<ISettingsService>();
            settingsService.Settings = new Settings
            {
                Address = NetworkUtils.GetLocalIPAddress().ToString(),
                Port = 9977,
                InactiveTime = 20,
                RemoveTime = 60,
                StartMinimized = false
            };

            try
            {
                settingsService.Load();
            }
            catch (Exception exc)
            {
                logger.Error(exc, "Can not load settings. Used default values");
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var logger = Container.Resolve<ILoggerService>();
            logger.Info("App finished");
        }
    }
}