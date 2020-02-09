using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Core.Navigation;
using JToolbox.XamarinForms.Dialogs;
using Prism.Commands;

namespace RemoteControl.MobileClient.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string statusText;

        private readonly IDialogsService dialogsService;

        public MainViewModel(INavService navService, IDialogsService dialogsService)
            : base(navService)
        {
            this.dialogsService = dialogsService;

            SetDisconnectedStatus();
        }

        public DelegateCommand SettingsCommand => new DelegateCommand(async () =>
        {
            await navService.NavigateToViewModel<SettingsViewModel>();
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