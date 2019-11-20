using RemoteControl.Proxy;

namespace RemoteControl.Server.RemoteCommands
{
    public interface IRemoteCommandsService : IProxyServer
    {
        event OnStop OnStop;
        event OnStart OnStart;
    }
}