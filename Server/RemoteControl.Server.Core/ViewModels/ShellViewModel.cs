using JToolbox.Core.Utilities;
using JToolbox.WPF.Core.Awareness;
using Prism.Mvvm;
using RemoteControl.Server.Connections;
using RemoteControl.Server.Core.Services;
using RemoteControl.Server.Messages;
using RemoteControl.Server.RemoteCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private ObservableCollection<ConnectionViewModel> connections = new ObservableCollection<ConnectionViewModel>();

        public ShellViewModel(IMessagesAggregator messagesAggregator, IRemoteCommandsService remoteCommandsService,
            IConnectionsService connectionsService, IShellDialogsService shellDialogsService)
        {
            this.shellDialogsService = shellDialogsService;
            this.remoteCommandsService = remoteCommandsService;

            InitializeConnections(connectionsService);
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
            set
            {
                SetProperty(ref connectionStatus, value);
                RaisePropertyChanged(nameof(AppInfo));
            }
        }

        public string AppInfo
        {
            get => $"Remote Control - {ConnectionStatus} - {ConnectionsCounter}";
        }

        public ObservableCollection<ConnectionViewModel> Connections
        {
            get => connections;
            set => SetProperty(ref connections, value);
        }

        public string ConnectionsCounter { get; set; }

        public int ActiveConnections
        {
            get => activeConnections;
            set => UpdateConnections(ref activeConnections, value);
        }

        public int InactiveConnections
        {
            get => inactiveConnections;
            set => UpdateConnections(ref inactiveConnections, value);
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

        private void UpdateConnections(ref int connectionsCounter, int value)
        {
            connectionsCounter = value;
            UpdateConnections();
        }

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
                await remoteCommandsService.Start(NetworkUtils.GetLocalIPAddress().ToString(), 7890);
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

        private void RemoteCommandsService_OnStart(string address, int port)
        {
            AppendLog("Server started");
            ConnectionStatus = $"Listening at {address}:{port}";
        }

        private void InitializeConnectionStatus()
        {
            ConnectionStatus = "Listening stopped";
        }

        private void UpdateConnections()
        {
            ConnectionsCounter = $"Connections: {activeConnections}/{inactiveConnections}";
            RaisePropertyChanged(nameof(ConnectionsCounter));
            RaisePropertyChanged(nameof(AppInfo));
        }

        private void InitializeConnections(IConnectionsService connectionsService)
        {
            connectionsService.OnConnectionsStatusChanged += ConnectionsService_OnConnectionsStatusChanged;
            connectionsService.OnNewConnection += ConnectionsService_OnNewConnection;
            connectionsService.OnConnectionRemove += ConnectionsService_OnConnectionRemove;
        }

        private void ConnectionsService_OnConnectionRemove(Connection connection)
        {
            AppendLog($"{connection.ConnectionRequest.Name} removed");
        }

        private void ConnectionsService_OnNewConnection(Connection newConnection)
        {
            AppendLog($"{newConnection.ConnectionRequest.Name} from {newConnection.ConnectionRequest.Address} connected");
        }

        private void ConnectionsService_OnConnectionsStatusChanged(List<Connection> connections)
        {
            Connections = new ObservableCollection<ConnectionViewModel>(connections.Select(c => new ConnectionViewModel(c)));
            ActiveConnections = Connections.Count(c => c.Active);
            InactiveConnections = Connections.Count - ActiveConnections;
            Connections.OrderBy(c => c.UpdateTime);
        }
    }
}