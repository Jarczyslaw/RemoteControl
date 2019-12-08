using JToolbox.Desktop.Core;
using Prism.Commands;
using Prism.Mvvm;
using RemoteControl.Server.AppSettings;
using RemoteControl.Server.Core.Services;
using System;
using System.IO;

namespace RemoteControl.Server.Core.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        private string address;
        private int port, inactiveTime, removeTime;
        private bool runAtStartup, startMinimized;
        private readonly ISettingsService settingsService;
        private readonly IShellDialogsService shellDialogsService;
        private readonly string appName = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
        private readonly string appLocation = System.Reflection.Assembly.GetEntryAssembly().Location;

        public SettingsViewModel(ISettingsService settingsService, IShellDialogsService shellDialogsService)
        {
            this.settingsService = settingsService;
            this.shellDialogsService = shellDialogsService;
            LoadSettings();
        }


        public DelegateCommand SaveCommand => new DelegateCommand(() =>
        {

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
    }
}