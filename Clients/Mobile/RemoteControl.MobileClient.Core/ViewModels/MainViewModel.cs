using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Core.Navigation;
using JToolbox.XamarinForms.Dialogs;
using Prism.Commands;
using Prism.Navigation;

namespace RemoteControl.MobileClient.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string statusText;

        private IDialogsService dialogsService;

        public MainViewModel(INavService navService, INavigationService navigationService,
            IDialogsService dialogsService)
            : base(navService, navigationService)
        {
            this.dialogsService = dialogsService;

            SetDisconnectedStatus();
        }

        public DelegateCommand ConnectCommand => new DelegateCommand(() =>
        {
            dialogsService.Toast("ConnectCommand");
        });

        public DelegateCommand DisconnectCommand => new DelegateCommand(() =>
        {
            dialogsService.Toast("DisconnectCommand");
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