using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Core.Navigation;
using JToolbox.XamarinForms.Dialogs;
using Prism.Commands;
using Prism.Navigation;
using RemoteControl.MobileClient.Core.Themes;
using RemoteControl.MobileClient.Core.Themes.ThemesResources;

namespace RemoteControl.MobileClient.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string statusText;

        private IDialogsService dialogsService;
        private IThemeManager themeManager;

        public MainViewModel(INavService navService, INavigationService navigationService,
            IDialogsService dialogsService, IThemeManager themeManager)
            : base(navService, navigationService)
        {
            this.dialogsService = dialogsService;
            this.themeManager = themeManager;

            SetDisconnectedStatus();
        }

        public DelegateCommand ConnectCommand => new DelegateCommand(() =>
        {
            dialogsService.Toast("ConnectCommand");
            themeManager.SetTheme<LightTheme>();
        });

        public DelegateCommand DisconnectCommand => new DelegateCommand(() =>
        {
            dialogsService.Toast("DisconnectCommand");
            themeManager.SetTheme<DarkTheme>();
        });

        public DelegateCommand ShutdownCommand => new DelegateCommand(() =>
        {
            dialogsService.Toast("ShutdownCommand");
        });

        public DelegateCommand RestartCommand => new DelegateCommand(() =>
        {
            dialogsService.Toast("RestartCommand");
        });

        public string StatusText
        {
            get => statusText;
            set => SetProperty(ref statusText, value);
        }

        private void SetConnectedStatus(string address, int port)
        {
            StatusText = $"Connected to: {address} at port: {port}";
        }

        private void SetDisconnectedStatus()
        {
            StatusText = "Disconnected";
        }
    }
}