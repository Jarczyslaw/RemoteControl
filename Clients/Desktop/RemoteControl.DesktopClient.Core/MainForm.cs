using JToolbox.Core.Utilities;
using JToolbox.Desktop.Dialogs;
using RemoteControl.Proxy;
using System;
using System.Windows.Forms;
using JToolbox.Core.Extensions;
using static RemoteControl.Proxy.RequestBase.Types;

namespace RemoteControl.DesktopClient.Core
{
    public partial class MainForm : Form
    {
        private readonly LazyProxyClient lazyProxyClient = new LazyProxyClient();
        private readonly IDialogsService dialogsService;

        public MainForm(IDialogsService dialogsService)
        {
            this.dialogsService = dialogsService;

            InitializeComponent();
            InitializeClientData();
        }

        public string DeviceName
        {
            get => tbDeviceName.Text;
            set => tbDeviceName.Text = value;
        }

        public DeviceType DeviceType
        {
            set => tbDeviceType.Text = value.ToString();
            get => DeviceType.Desktop;
        }

        public string LocalAddress
        {
            get => tbLocalAddress.Text;
            set => tbLocalAddress.Text = value;
        }

        public string RemoteAddress
        {
            get => tbRemoteAddress.Text;
            set => tbRemoteAddress.Text = value;
        }

        public int RemotePort
        {
            get => Convert.ToInt32(nudRemotePort.Value);
            set => nudRemotePort.Value = value;
        }

        public RequestBase RequestBase => new RequestBase
        {
            Address = LocalAddress,
            Name = DeviceName,
            Type = DeviceType
        };

        private void InitializeClientData()
        {
            DeviceName = Environment.MachineName;
            DeviceType = DeviceType.Desktop;
            RemoteAddress =
                LocalAddress = NetworkUtils.GetLocalIPAddress()
                .ToString();
            RemotePort = 7890;
        }

        private async void btnPing_Click(object sender, EventArgs e)
        {
            try
            {
                var testMessage = "Test message";
                var proxy = await lazyProxyClient.GetProxyClient(RemoteAddress, RemotePort);
                var response = await proxy.Client.PingAsync(new PingRequest
                {
                    Message = testMessage
                });

                if (response.ResponseBase.HasError())
                {
                    dialogsService.ShowError(response.ResponseBase.Error);
                    return;
                }

                if (response.ResponseMessage == testMessage)
                {
                    dialogsService.ShowInfo($"Successfully received message from server: {response.ResponseMessage}");
                }
                else
                {
                    dialogsService.ShowError($"Invalid message from server: {response.ResponseMessage}");
                }
            }
            catch (Exception exc)
            {
                dialogsService.ShowException(exc);
            }
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                var proxy = await lazyProxyClient.GetProxyClient(RemoteAddress, RemotePort);
                var response = await proxy.Client.DisconnectAsync(new DisconnectRequest
                {
                    RequestBase = RequestBase
                });
                if (response.ResponseBase.HasError())
                {
                    dialogsService.ShowError(response.ResponseBase.Error);
                }
            }
            catch (Exception exc)
            {
                dialogsService.ShowException(exc);
            }
        }

        private async void btnShutdown_Click(object sender, EventArgs e)
        {
            try
            {
                var proxy = await lazyProxyClient.GetProxyClient(RemoteAddress, RemotePort);
                if (dialogsService.ShowYesNoQuestion("Do you really want to shutdown remote machine?"))
                {
                    await proxy.Client.ShutdownAsync(new ShutdownRequest
                    {
                        RequestBase = RequestBase
                    });
                }
            }
            catch (Exception exc)
            {
                dialogsService.ShowException(exc);
            }
        }

        private async void btnRestart_Click(object sender, EventArgs e)
        {
            try
            {
                var proxy = await lazyProxyClient.GetProxyClient(RemoteAddress, RemotePort);
                if (dialogsService.ShowYesNoQuestion("Do you really want to restart remote machine?"))
                {
                    await proxy.Client.RestartAsync(new RestartRequest
                    {
                        RequestBase = RequestBase
                    });
                }
            }
            catch (Exception exc)
            {
                dialogsService.ShowException(exc);
            }
        }

        private async void btnSysInfo_Click(object sender, EventArgs e)
        {
            try
            {
                var proxy = await lazyProxyClient.GetProxyClient(RemoteAddress, RemotePort);
                var info = await proxy.Client.GetSystemInformationAsync(new GetSystemInformationRequest
                {
                    RequestBase = RequestBase
                });
                dialogsService.ShowInfo(info.SystemInformation.PublicPropertiesToString());
            }
            catch (Exception exc)
            {
                dialogsService.ShowException(exc);
            }
        }
    }
}