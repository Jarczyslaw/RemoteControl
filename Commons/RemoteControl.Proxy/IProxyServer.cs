using Grpc.Core;
using System.Threading.Tasks;

namespace RemoteControl.Proxy
{
    public interface IProxyServer
    {
        Server Server { get; }

        bool IsListening { get; }
        int? Port { get; }
        string Address { get; }

        Task Start(string address, int port);

        Task Stop();
    }
}