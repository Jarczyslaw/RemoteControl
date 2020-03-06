using JToolbox.Core.Utilities;
using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using Prism.Commands;
using Prism.Navigation;
using RemoteControl.MobileClient.Core.Services;
using RemoteControl.Proxy;
using System;
using System.Threading.Tasks;
using System.Linq;
using static RemoteControl.Proxy.RequestBase.Types;

namespace RemoteControl.MobileClient.Core.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IDialogsService dialogsService;
        private readonly IAppSettings appSettings;

        private string name;
        private string localAddress;
        private string remoteAddress;
        private int port;

        public SettingsViewModel(INavigationService navigationService, IDialogsService dialogsService, IAppSettings appSettings)
            : base(navigationService)
        {
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
            dialogsService.Toast("Saved successfully");
        });

        public DelegateCommand SearchAddressCommand => new DelegateCommand(async () =>
        {
            var localAddresses = NetworkUtils.GetLocalIPAddresses()
                .Select(a => a.ToString())
                .ToArray();
            var selectedAddress = await dialogsService.UserDialogs.ActionSheetAsync("Addresses", null, null, null, localAddresses);
            if (!string.IsNullOrEmpty(selectedAddress))
            {
                LocalAddress = selectedAddress;
            }
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
                var response = await proxyClient.Client.PingAsync(new PingRequest
                {
                    Message = "Test message"
                });

                if (response.ResponseBase.HasError())
                {
                    await dialogsService.Error(response.ResponseBase.Error);
                    return;
                }

                await dialogsService.Information("Successfully received message from server");
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