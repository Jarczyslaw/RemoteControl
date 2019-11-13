using JToolbox.Desktop.Core.Services;
using JToolbox.Desktop.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using RemoteControl.Server.Core.Services;
using System;

namespace RemoteControl.Server.Core.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private string logs;
        private readonly ISystemService systemService;
        private readonly IShellDialogsService shellDialogsService;
        private readonly IDialogsService dialogsService;

        public ShellViewModel(ISystemService systemService, IShellDialogsService shellDialogsService, IDialogsService dialogsService)
        {
            this.systemService = systemService;
            this.shellDialogsService = shellDialogsService;
            this.dialogsService = dialogsService;
        }

        public string Logs
        {
            get => logs;
            set => SetProperty(ref logs, value);
        }

        public DelegateCommand ShutdownCommand => new DelegateCommand(async () =>
        {
            if (await shellDialogsService.ShowYesNoQuestion("Do you really want o shutdown this machine?"))
            {
                systemService.Shutdown();
            }
        });

        private void AppendLog(string message)
        {
            Logs = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] - {message}"
                + Environment.NewLine + Logs;
        }
    }
}