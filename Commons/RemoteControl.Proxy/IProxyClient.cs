using Grpc.Core;
using System.Threading.Tasks;

namespace RemoteControl.Proxy
{
    public interface IProxyClient
    {
        Channel Channel { get; }

        ProxyService.ProxyServiceClient Client { get; }

        Task Start(string address, int port);

        Task Stop();
    }
}