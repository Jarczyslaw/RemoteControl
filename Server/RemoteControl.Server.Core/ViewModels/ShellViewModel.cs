using JToolbox.Desktop.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using RemoteControl.Server.Core.Services;
using RemoteControl.Server.RemoteCommands;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace RemoteControl.Server.Core.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private string logs;
        private string message;
        private readonly IRemoteCommandsService remoteCommandsService;
        private readonly IShellDialogsService shellDialogsService;
        private readonly IDialogsService dialogsService;

        public ShellViewModel(IRemoteCommandsService remoteCommandsService, IShellDialogsService shellDialogsService, IDialogsService dialogsService)
        {
            this.remoteCommandsService = remoteCommandsService;
            this.shellDialogsService = shellDialogsService;
            this.dialogsService = dialogsService;
        }

        public string Logs
        {
            get => logs;
            set => SetProperty(ref logs, value);
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

        public DelegateCommand ShutdownCommand => new DelegateCommand(async () =>
        {
            try
            {
                if (await shellDialogsService.ShowYesNoQuestion("Do you really want o shutdown this machine?"))
                {
                    remoteCommandsService.Shutdown();
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
                    remoteCommandsService.Restart();
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
                var screenshot = remoteCommandsService.CapturePrimaryScreen();
                if (SaveScreenshotFile(screenshot))
                {
                    Message = "Primary screen screenshot saved";
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
                var screenshot = remoteCommandsService.CaptureAllScreens();
                if (SaveScreenshotFile(screenshot))
                {
                    Message = "All screens screenshot saved";
                }
            }
            catch (Exception exc)
            {
                await shellDialogsService.ShowException(exc);
            }
        });

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
                bitmap.Save(screenshotFile);
                return true;
            }
            return false;
        }
    }
}