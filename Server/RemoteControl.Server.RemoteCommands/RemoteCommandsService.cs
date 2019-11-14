using JToolbox.Desktop.Core;
using JToolbox.Desktop.Core.Services;
using JToolbox.WPF.Core.Extensions;
using System.Drawing;

namespace RemoteControl.Server.RemoteCommands
{
    public class RemoteCommandsService : IRemoteCommandsService
    {
        private readonly ISystemService systemService;
        private readonly ScreenCapture screenCapture = new ScreenCapture();
        public RemoteCommandsService(ISystemService systemService)
        {
            this.systemService = systemService;
        }

        public void Shutdown()
        {
            systemService.Shutdown();
        }

        public void Restart()
        {
            systemService.Restart();
        }

        public Bitmap CapturePrimaryScreen()
        {
            return screenCapture.CapturePrimaryScreen();
        }

        public Bitmap CaptureAllScreens()
        {
            return screenCapture.CaptureAllScreens();
        }
    }
}