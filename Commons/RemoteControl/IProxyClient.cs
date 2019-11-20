using System.Threading.Tasks;

namespace RemoteControl
{
    public interface IProxyClient
    {
        Task Start(string address, int port);

        Task Stop();
    }
}