﻿using JToolbox.Core.Utilities;
using JToolbox.Desktop.Dialogs;
using RemoteControl.Proxy;
using System;
using System.Windows.Forms;
using static RemoteControl.Proxy.ConnectionRequest.Types;

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

        public ConnectionRequest ConnectionRequest => new ConnectionRequest
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

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                var proxy = await lazyProxyClient.GetProxyClient(RemoteAddress, RemotePort);
                var response = await proxy.Client.ConnectAsync(ConnectionRequest);
                if (response.HasError())
                {
                    dialogsService.ShowError(response.Error);
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
                var response = await proxy.Client.DisconnectAsync(ConnectionRequest);
                if (response.HasError())
                {
                    dialogsService.ShowError(response.Error);
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
                    await proxy.Client.ShutdownAsync(ConnectionRequest);
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
                    await proxy.Client.RestartAsync(ConnectionRequest);
                }
            }
            catch (Exception exc)
            {
                dialogsService.ShowException(exc);
            }
        }
    }
}