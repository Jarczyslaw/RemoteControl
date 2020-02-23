using JToolbox.Core.Extensions;
using JToolbox.Desktop.Core;
using JToolbox.Desktop.Core.Services;
using JToolbox.Desktop.Dialogs;
using JToolbox.SysInformation;
using JToolbox.WPF.Core.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using RemoteControl.Server.Core.Services;
using RemoteControl.Server.Messages;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace RemoteControl.Server.Core.ViewModels
{
    public class ControlPanelViewModel : BindableBase
    {
        private readonly ISystemService systemService;
        private readonly IShellDialogsService shellDialogsService;
        private readonly IDialogsService dialogsService;
        private readonly IMessagesAggregator messagesAggregator;
        private readonly ScreenCapture screenCapture = new ScreenCapture();

        public ControlPanelViewModel(ISystemService systemService, IMessagesAggregator messagesAggregator,
            IShellDialogsService shellDialogsService, IDialogsService dialogsService)
        {
            this.systemService = systemService;
            this.shellDialogsService = shellDialogsService;
            this.dialogsService = dialogsService;
            this.messagesAggregator = messagesAggregator;
        }

        public DelegateCommand GetSystemInfoCommand => new DelegateCommand(async () =>
        {
            await Task.Run(() =>
            {
                var osInfo = SystemInformation.GetOSInfo();
                var cpuInfo = SystemInformation.GetCPUInfo();
                var memoryInfo = SystemInformation.GetMemoryInfo();

                var sysInfo = new SysInformation
                {
                    OSBuildNumber = osInfo.BuildNumber,
                    OSName = osInfo.Name,
                    OSVersion = osInfo.Version,
                    CPUCaption = cpuInfo.Caption,
                    CPUName = cpuInfo.Name,
                    NumberOfCores = cpuInfo.NumberOfEnabledCores,
                    NumberOfLogicalProcessors = cpuInfo.NumberOfLogicalProcessors,
                    FreeMemory = memoryInfo.FreePhysicalMemory,
                    TotalMemory = memoryInfo.TotalVisibleMemory
                };
                dialogsService.ShowInfo(sysInfo.PublicPropertiesToString());
            });
        });

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
                        messagesAggregator.StatusMessage("Primary screen screenshot saved");
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
                        messagesAggregator.StatusMessage("All screens screenshot saved");
                    }
                }
            }
            catch (Exception exc)
            {
                await shellDialogsService.ShowException(exc);
            }
        });

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

        private class SysInformation
        {
            public string OSBuildNumber { get; set; }
            public string OSName { get; set; }
            public string OSVersion { get; set; }
            public string CPUCaption { get; set; }
            public string CPUName { get; set; }
            public int NumberOfCores { get; set; }
            public int NumberOfLogicalProcessors { get; set; }
            public ulong FreeMemory { get; set; }
            public ulong TotalMemory { get; set; }
        }
    }
}