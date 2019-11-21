using JToolbox.Desktop.Core;
using JToolbox.Desktop.Core.Services;
using JToolbox.Desktop.Dialogs;
using JToolbox.WPF.Core.Awareness;
using JToolbox.WPF.Core.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using RemoteControl.Server.Core.Services;
using RemoteControl.Server.RemoteCommands;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows;

namespace RemoteControl.Server.Core.ViewModels
{
    public class ShellViewModel : BindableBase, IOnLoadedAware, ICloseSource
    {
        private string logs;
        private string message;
        private string connectionStatus;
        private int activeConnections, inactiveConnections;
        private readonly IShellDialogsService shellDialogsService;
        private readonly IDialogsService dialogsService;
        private readonly ISystemService systemService;
        private readonly IRemoteCommandsService remoteCommandsService;
        private readonly ILogsAggregator logsAggregator;
        private readonly ScreenCapture screenCapture = new ScreenCapture();

        public ShellViewModel(ILogsAggregator logsAggregator, IRemoteCommandsService remoteCommandsService, ISystemService systemService, IShellDialogsService shellDialogsService, IDialogsService dialogsService)
        {
            this.shellDialogsService = shellDialogsService;
            this.dialogsService = dialogsService;
            this.systemService = systemService;
            this.remoteCommandsService = remoteCommandsService;
            this.logsAggregator = logsAggregator;

            Initialize(remoteCommandsService);
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

        public string Message
        {
            get => message;
            set
            {
                SetProperty(ref message, value);
                if (!string.IsNullOrEmpty(value))
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(5000);
                        Message = string.Empty;
                    });
                }
            }
        }

        public Action OnClose { get; set; }

        public DelegateCommand ShutdownCommand => new DelegateCommand(async () =>
        {
            try
            {
                if (await shellDialogsService.ShowYesNoQuestion("Do you really want o shutdown this machine?"))
                {
                    systemService.Shutdown();
                }
            }
            catch (Exception exc)
            {
                await shellDialogsService.ShowException(exc);
            }
        });

        public DelegateCommand RestartCommand => new DelegateCommand(async () =>
        {
            try
            {
                if (await shellDialogsService.ShowYesNoQuestion("Do you really want o restart this machine?"))
                {
                    systemService.Restart();
                }
            }
            catch (Exception exc)
            {
                await shellDialogsService.ShowException(exc);
            }
        });

        public DelegateCommand CapturePrimaryScreenCommand => new DelegateCommand(async () =>
        {
            try
            {
                using (var screenshot = screenCapture.CapturePrimaryScreen())
                {
                    if (SaveScreenshotFile(screenshot))
                    {
                        Message = "Primary screen screenshot saved";
                    }
                }
            }
            catch (Exception exc)
            {
                await shellDialogsService.ShowException(exc);
            }
        });

        public DelegateCommand CaptureAllScreensCommand => new DelegateCommand(async () =>
        {
            try
            {
                using (var screenshot = screenCapture.CaptureAllScreens())
                {
                    if (SaveScreenshotFile(screenshot))
                    {
                        Message = "All screens screenshot saved";
                    }
                }
            }
            catch (Exception exc)
            {
                await shellDialogsService.ShowException(exc);
            }
        });

        private void Initialize(IRemoteCommandsService remoteCommandsService)
        {
            remoteCommandsService.OnStart += RemoteCommandsService_OnStart;
            remoteCommandsService.OnStop += RemoteCommandsService_OnStop;
            InitializeConnectionStatus();
            UpdateConnections();
        }

        private void AppendLog(string message)
        {
            Logs = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] - {message}"
                + Environment.NewLine + Logs;
        }

        private bool SaveScreenshotFile(Bitmap bitmap)
        {
            var screenshotFile = dialogsService.SaveFile("Screen shot", null, $"screenshot_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}", new DialogFilterPair
            {
                DisplayName = "png",
                ExtensionsList = "png"
            });
            if (!string.IsNullOrEmpty(screenshotFile))
            {
                bitmap.Save(screenshotFile, ImageFormat.Png);
                return true;
            }
            return false;
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