using System.Drawing;

namespace RemoteControl.Server.RemoteCommands
{
    public interface IRemoteCommandsService
    {
        void Restart();
        void Shutdown();
        Bitmap TakeAppScreenshot();
        Bitmap TakeScreenshot();
    }
}