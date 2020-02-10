using Acr.UserDialogs;
using JToolbox.Core.Abstraction;
using JToolbox.XamarinForms.Core.Abstraction;
using JToolbox.XamarinForms.Core.Navigation;
using JToolbox.XamarinForms.Dialogs;
using JToolbox.XamarinForms.Logging;
using JToolbox.XamarinForms.Permissions;
using Prism;
using Prism.Ioc;
using RemoteControl.MobileClient.Core.Services;
using RemoteControl.MobileClient.Core.ViewModels;
using RemoteControl.Proxy;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace RemoteControl.MobileClient.Core
{
    public partial class App
    {
        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        public static IContainerProvider ContainerProvider { get; private set; }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            ContainerProvider = Container;
            await StartNavigation();
        }

        private async Task StartNavigation()
        {
            await NavigationService.StartNavigationViewModel<MainViewModel>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterDependencies(containerRegistry);
            RegisterViews(containerRegistry);
        }

        private void RegisterDependencies(IContainerRegistry containerRegistry)
        {
            RegisterLogger(containerRegistry);
            containerRegistry.RegisterInstance(UserDialogs.Instance);
            containerRegistry.RegisterSingleton<IDialogsService, DialogsService>();
            containerRegistry.RegisterSingleton<IPermissionsService, PermissionsService>();
            containerRegistry.RegisterSingleton<ILazyProxyClient, LazyProxyClient>();
            containerRegistry.RegisterSingleton<IAppSettings, AppSettings>();
        }

        private void RegisterViews(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            NavigationMapper.Instance.Register(containerRegistry, Assembly.GetExecutingAssembly());
        }

        private void RegisterLogger(IContainerRegistry containerRegistry)
        {
            var appConfig = Container.Resolve<IAppCore>();
            containerRegistry.RegisterInstance<ILoggerService>(new LoggerService(appConfig.LogPath));
        }
    }
}