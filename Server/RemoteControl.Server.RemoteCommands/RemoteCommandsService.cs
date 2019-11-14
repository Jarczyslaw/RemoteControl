using JToolbox.Desktop.Core;
using JToolbox.Desktop.Core.Services;
using System.Drawing;

namespace RemoteControl.Server.RemoteCommands
{
    public class RemoteCommandsService : IRemoteCommandsService
    {
        private readonly ISystemService systemService;

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

        public Bitmap TakeScreenshot()
        {
            return ScreenCapture.CaptureDesktop();
        }

        public Bitmap TakeAppScreenshot()
        {
            return ScreenCapture.CaptureActiveWindow();
        }
    }
}