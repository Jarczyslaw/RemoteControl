using System.Threading.Tasks;

namespace RemoteControl
{
    public interface IProxyServer
    {
        Task Start(int port);

        Task Stop();
    }
}