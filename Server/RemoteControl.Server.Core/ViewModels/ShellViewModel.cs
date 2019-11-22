using JToolbox.WPF.Core.Awareness;
using Prism.Mvvm;
using RemoteControl.Server.Core.Services;
using RemoteControl.Server.RemoteCommands;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace RemoteControl.Server.Core.ViewModels
{
    public class ShellViewModel : BindableBase, IOnLoadedAware, ICloseSource
    {
        private string logs;
        private string statusMessage;
        private string connectionStatus;
        private int activeConnections, inactiveConnections;
        private readonly IShellDialogsService shellDialogsService;
        private readonly IRemoteCommandsService remoteCommandsService;

        public ShellViewModel(IMessagesAggregator messagesAggregator, IRemoteCommandsService remoteCommandsService, IShellDialogsService shellDialogsService)
        {
            this.shellDialogsService = shellDialogsService;
            this.remoteCommandsService = remoteCommandsService;

            InitializeCommands(remoteCommandsService);
            InitializeMessages(messagesAggregator);
        }

        public string Logs
        {
            get => logs;
            set => SetProperty(ref logs, value);
        }

        public string ConnectionStatus
        {
            get => connectionStatus;
            set => SetProperty(ref connectionStatus, value);
        }

        public string Connections { get; set; }

        public int ActiveConnections
        {
            get => activeConnections;
            set
            {
                activeConnections = value;
                UpdateConnections();
            }
        }

        public int InactiveConnections
        {
            get => inactiveConnections;
            set
            {
                inactiveConnections = value;
                UpdateConnections();
            }
        }

        public string StatusMessage
        {
            get => statusMessage;
            set
            {
                SetProperty(ref statusMessage, value);
                if (!string.IsNullOrEmpty(value))
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(5000);
                        StatusMessage = string.Empty;
                    });
                }
            }
        }

        public Action OnClose { get; set; }

        private void InitializeCommands(IRemoteCommandsService remoteCommandsService)
        {
            remoteCommandsService.OnStart += RemoteCommandsService_OnStart;
            remoteCommandsService.OnStop += RemoteCommandsService_OnStop;
            InitializeConnectionStatus();
            UpdateConnections();
        }

        private void InitializeMessages(IMessagesAggregator messagesAggregator)
        {
            messagesAggregator.OnMessage += AppendLog;
            messagesAggregator.OnStatusMessage += m => StatusMessage = m;
        }

        private void AppendLog(string message)
        {
            Logs = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] - {message}"
                + Environment.NewLine + Logs;
        }

        public async void OnLoaded()
        {
            try
            {
                await remoteCommandsService.Start(7890);
            }
            catch (Exception exc)
            {
                await shellDialogsService.ShowException("Initialization error occured. Application will be closed", exc);
                Application.Current.Shutdown();
            }
        }

        private void RemoteCommandsService_OnStop()
        {
            AppendLog("Server stopped");
            InitializeConnectionStatus();
        }

        private void RemoteCommandsService_OnStart(int port)
        {
            AppendLog("Server started");
            ConnectionStatus = $"Listening at port: {port}";
        }

        private void InitializeConnectionStatus()
        {
            ConnectionStatus = "Listening stopped";
        }

        private void UpdateConnections()
        {
            Connections = $"Connections: {activeConnections}/{inactiveConnections}";
            RaisePropertyChanged(nameof(Connections));
        }
    }
}