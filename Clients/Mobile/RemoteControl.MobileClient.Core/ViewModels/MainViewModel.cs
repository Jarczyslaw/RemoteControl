using JToolbox.Core.Extensions;
using JToolbox.Core.Utilities;
using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Core.Navigation;
using JToolbox.XamarinForms.Dialogs;
using Prism.Commands;
using Prism.Navigation;
using RemoteControl.MobileClient.Core.Services;
using RemoteControl.Proxy;
using System;
using System.Threading.Tasks;

namespace RemoteControl.MobileClient.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string statusText;

        private readonly IDialogsService dialogsService;
        private readonly ILazyProxyClient lazyProxyClient;
        private readonly IAppSettings appSettings;

        public MainViewModel(ILazyProxyClient lazyProxyClient, INavigationService navigationService,
            IDialogsService dialogsService, IAppSettings appSettings)
            : base(navigationService)
        {
            this.dialogsService = dialogsService;
            this.lazyProxyClient = lazyProxyClient;
            this.appSettings = appSettings;

            SetDisconnectedStatus();
        }

        public DelegateCommand SettingsCommand => new DelegateCommand(async () => await navigationService.NavigateToViewModel<SettingsViewModel>());

        public DelegateCommand GetServerInfoCommand => new DelegateCommand(async () =>
        {
            await ExecuteRemoteCommand(async (client) =>
            {
                var response = await client.Client.GetSystemInformationAsync(new GetSystemInformationRequest
                {
                    RequestBase = GetRequestBase()
                });

                if (response.ResponseBase.HasError())
                {
                    await dialogsService.Error(response.ResponseBase.Error);
                    return false;
                }

                await dialogsService.Information(response.SystemInformation.PublicPropertiesToString());
                return true;
            });
        });

        public DelegateCommand ShutdownCommand => new DelegateCommand(async () =>
        {
            var dialogResult = await dialogsService.QuestionYesNo("Do you really want to shutdown remote machine?");
            if (dialogResult)
            {
                await ExecuteRemoteCommand(async (client) =>
                {
                    var response = await client.Client.ShutdownAsync(new ShutdownRequest
                    {
                        RequestBase = GetRequestBase()
                    });

                    if (response.ResponseBase.HasError())
                    {
                        await dialogsService.Error(response.ResponseBase.Error);
                        return false;
                    }

                    await dialogsService.Information("Remote machine should now be turned off");
                    return true;
                });
            }
        });

        public DelegateCommand RestartCommand => new DelegateCommand(async () =>
        {
            var dialogResult = await dialogsService.QuestionYesNo("Do you really want to restart remote machine?");
            if (!dialogResult)
            {
                await ExecuteRemoteCommand(async (client) =>
                {
                    var response = await client.Client.RestartAsync(new RestartRequest
                    {
                        RequestBase = GetRequestBase()
                    });

                    if (response.ResponseBase.HasError())
                    {
                        await dialogsService.Error(response.ResponseBase.Error);
                        return false;
                    }

                    await dialogsService.Information("Remote machine should now be restarted");
                    return true;
                });
            }
        });

        private async Task ExecuteRemoteCommand(Func<ProxyClient, Task<bool>> remoteCommandAction)
        {
            try
            {
                if (!ValidateAppSettings())
                {
                    return;
                }

                await dialogsService.ShowLoading("Executing command...", async () =>
                {
                    var client = await lazyProxyClient.GetProxyClient(appSettings.RemoteAddress, appSettings.Port);
                    var remoteCommandActionResult = await remoteCommandAction(client);
                    if (remoteCommandActionResult)
                    {
                        UpdateConnectedStatus(true);
                    }
                });
            }
            catch (Exception exc)
            {
                UpdateConnectedStatus(false);
                await dialogsService.Error(exc.Message);
            }
        }

        public string StatusText
        {
            get => statusText;
            set => SetProperty(ref statusText, value);
        }

        private void UpdateConnectedStatus(bool connected)
        {
            if (connected)
            {
                SetConnectedStatus(appSettings.RemoteAddress, appSettings.Port);
            }
            else
            {
                SetDisconnectedStatus();
            }
        }

        private void SetConnectedStatus(string address, int port)
        {
            StatusText = $"Connected to: {address} at port: {port}";
        }

        private void SetDisconnectedStatus()
        {
            StatusText = "Disconnected";
        }

        private RequestBase GetRequestBase()
        {
            return new RequestBase
            {
                Address = NetworkUtils.GetLocalIPAddress().ToString(),
                Type = RequestBase.Types.DeviceType.Mobile,
                Name = appSettings.Name
            };
        }

        private bool ValidateAppSettings()
        {
            var result = appSettings.Validate();
            if (!string.IsNullOrEmpty(result))
            {
                dialogsService.Error(result);
                return false;
            }
            return true;
        }
    }
}