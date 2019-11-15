using System.Threading.Tasks;

namespace RemoteControl.Server.RemoteCommands
{
    public interface IRemoteCommandsService
    {
        Task Start(int port);
        Task Stop();
    }
}