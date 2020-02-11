using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using Prism.Commands;
using Prism.Navigation;
using RemoteControl.MobileClient.Core.Services;
using RemoteControl.Proxy;
using System;
using System.Threading.Tasks;
using static RemoteControl.Proxy.RequestBase.Types;

namespace RemoteControl.MobileClient.Core.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ILazyProxyClient lazyProxyClient;
        private readonly IDialogsService dialogsService;
        private readonly IAppSettings appSettings;

        private string name;
        private string localAddress;
        private string remoteAddress;
        private int port;

        public SettingsViewModel(INavigationService navigationService, ILazyProxyClient lazyProxyClient,
            IDialogsService dialogsService, IAppSettings appSettings)
            : base(navigationService)
        {
            this.lazyProxyClient = lazyProxyClient;
            this.dialogsService = dialogsService;
            this.appSettings = appSettings;

            LoadSettings();
        }

        public DelegateCommand SaveCommand => new DelegateCommand(async () =>
        {
            if (!await Validate())
            {
                return;
            }

            SaveSettings();
            await Close();
        });

        public DelegateCommand FindServerCommand => new DelegateCommand(async () =>
        {
        });

        public DelegateCommand CheckConnectionCommand => new DelegateCommand(async () =>
        {
            if (!await ValidateRemoteAddress())
            {
                return;
            }

            IProxyClient proxyClient = null;
            try
            {
                proxyClient = new ProxyClient();
                await proxyClient.Start(RemoteAddress, Port);
                var response = proxyClient.Client.PingAsync(new PingRequest
                {
                    Message = "Test message"
                });
            }
            catch (Exception exc)
            {
                await dialogsService.Error(exc.Message);
            }
            finally
            {
                await proxyClient?.Stop();
            }
        });

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Type
        {
            get => DeviceType.Mobile.ToString();
        }

        public string LocalAddress
        {
            get => localAddress;
            set => SetProperty(ref localAddress, value);
        }

        public string RemoteAddress
        {
            get => remoteAddress;
            set => SetProperty(ref remoteAddress, value);
        }

        public int Port
        {
            get => port;
            set => SetProperty(ref port, value);
        }

        private void SaveSettings()
        {
            appSettings.Name = Name;
            appSettings.Port = Port;
            appSettings.RemoteAddress = RemoteAddress;
        }

        private void LoadSettings()
        {
            Name = appSettings.Name;
            Port = appSettings.Port;
            RemoteAddress = appSettings.RemoteAddress;
        }

        private async Task<bool> ValidateRemoteAddress()
        {
            if (string.IsNullOrEmpty(RemoteAddress))
            {
                await dialogsService.Error("Remote address can not be empty");
                return false;
            }

            return true;
        }

        private async Task<bool> Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                await dialogsService.Error("Name can not be empty");
                return false;
            }

            return await ValidateRemoteAddress();
        }
    }
}