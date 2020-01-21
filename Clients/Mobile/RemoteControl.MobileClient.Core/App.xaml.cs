using Acr.UserDialogs;
using JToolbox.Core.Abstraction;
using JToolbox.XamarinForms.Core.Abstraction;
using JToolbox.XamarinForms.Core.Navigation;
using JToolbox.XamarinForms.Dialogs;
using JToolbox.XamarinForms.Logging;
using JToolbox.XamarinForms.Permissions;
using Prism;
using Prism.Ioc;
using RemoteControl.MobileClient.Core.Themes;
using RemoteControl.MobileClient.Core.ViewModels;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace RemoteControl.MobileClient.Core
{
    public partial class App
    {
        public static IContainerProvider ContainerProvider { get; private set; }
        private readonly INavService navService = new NavService();

        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
            ContainerProvider = Container;
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await navService.StartNavigationViewModel<MainViewModel>(NavigationService);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterDependencies(containerRegistry);
            RegisterViews(containerRegistry);
        }

        private void RegisterViews(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            navService.Register(containerRegistry, Assembly.GetExecutingAssembly());
        }

        private void RegisterDependencies(IContainerRegistry containerRegistry)
        {
            RegisterLogger(containerRegistry);
            containerRegistry.RegisterInstance(UserDialogs.Instance);
            containerRegistry.RegisterSingleton<IDialogsService, DialogsService>();
            containerRegistry.RegisterSingleton<IPermissionsService, PermissionsService>();
            containerRegistry.RegisterSingleton<IThemeManager, ThemeManager>();
            containerRegistry.RegisterInstance(navService);
        }

        private void RegisterLogger(IContainerRegistry containerRegistry)
        {
            var appConfig = Container.Resolve<IAppCore>();
            containerRegistry.RegisterInstance<ILoggerService>(new LoggerService(appConfig.LogPath));
        }
    }
}