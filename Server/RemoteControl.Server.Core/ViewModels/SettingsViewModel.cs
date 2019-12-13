using JToolbox.Desktop.Core;
using JToolbox.WPF.Core.Awareness;
using Prism.Commands;
using Prism.Mvvm;
using RemoteControl.Server.AppSettings;
using RemoteControl.Server.Connections;
using RemoteControl.Server.Core.Services;
using RemoteControl.Server.RemoteCommands;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace RemoteControl.Server.Core.ViewModels
{
    public class SettingsViewModel : BindableBase, ICloseSource
    {
        private string address;
        private int port, inactiveTime, removeTime;
        private bool runAtStartup, startMinimized;
        private readonly ISettingsService settingsService;
        private readonly IShellDialogsService shellDialogsService;
        private readonly IRemoteCommandsService remoteCommandsService;
        private readonly IConnectionsService connectionsService;
        private readonly string appName = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
        private readonly string appLocation = System.Reflection.Assembly.GetEntryAssembly().Location;
        private readonly string initialAddress;
        private readonly int initialPort;

        public SettingsViewModel(IRemoteCommandsService remoteCommandsService, IConnectionsService connectionsService,
            ISettingsService settingsService, IShellDialogsService shellDialogsService)
        {
            this.settingsService = settingsService;
            this.shellDialogsService = shellDialogsService;
            this.remoteCommandsService = remoteCommandsService;
            this.connectionsService = connectionsService;

            initialPort = settingsService.Settings.Port;
            initialAddress = settingsService.Settings.Address;

            LoadSettings();
        }

        public DelegateCommand SaveCommand => new DelegateCommand(async () =>
        {
            try
            {
                if (!Validate())
                {
                    return;
                }

                await SaveSettings();
                OnClose?.Invoke();
            }
            catch (Exception exc)
            {
                await shellDialogsService.ShowException(exc);
            }
        });

        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

        public int Port
        {
            get => port;
            set => SetProperty(ref port, value);
        }

        public int InactiveTime
        {
            get => inactiveTime;
            set => SetProperty(ref inactiveTime, value);
        }

        public int RemoveTime
        {
            get => removeTime;
            set => SetProperty(ref removeTime, value);
        }

        public bool RunAtStartup
        {
            get => runAtStartup;
            set => SetProperty(ref runAtStartup, value);
        }

        public bool StartMinimized
        {
            get => startMinimized;
            set => SetProperty(ref startMinimized, value);
        }

        public Action OnClose { get; set; }

        private void LoadSettings()
        {
            var settings = settingsService.Settings;
            Address = settings.Address;
            Port = settings.Port;
            InactiveTime = settings.InactiveTime;
            RemoveTime = settings.RemoveTime;
            StartMinimized = settings.StartMinimized;
            RunAtStartup = RegistryTools.CheckStartup(appName, appLocation);
        }

        private async Task SaveSettings()
        {
            if (initialPort != Port || initialAddress != Address)
            {
                if (connectionsService.Connections.Count != 0
                    && !await shellDialogsService.ShowYesNoQuestion("There are connected devices. Do you want to apply changes and restart remote service?"))
                {
                    return;
                }

                await remoteCommandsService.Stop();
                await remoteCommandsService.Start(Address, Port);
            }

            var settings = settingsService.Settings;
            settings.Address = Address;
            settings.Port = Port;
            settings.InactiveTime = InactiveTime;
            settings.RemoveTime = RemoveTime;
            settings.StartMinimized = StartMinimized;
            settingsService.Save();

            connectionsService.InactiveTime = TimeSpan.FromSeconds(InactiveTime);
            connectionsService.RemoveTime = TimeSpan.FromSeconds(RemoveTime);

            if (RunAtStartup)
            {
                RegistryTools.SetStartup(appName, appLocation);
            }
            else
            {
                RegistryTools.ClearStartup(appName);
            }
        }

        private bool Validate()
        {
            if (!IPAddress.TryParse(Address, out IPAddress ipAddress))
            {
                shellDialogsService.ShowError("Invalid address value");
                return false;
            }

            if (InactiveTime >= RemoveTime)
            {
                shellDialogsService.ShowError("Inactive time must be lower than Remove time");
                return false;
            }

            return true;
        }
    }
}