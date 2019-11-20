using Grpc.Core;
using System.Threading.Tasks;

namespace RemoteControl.Proxy
{
    public class ProxyServer : ProxyService.ProxyServiceBase, IProxyServer
    {
        public Server Server { get; private set; }

        public Task Start(int port)
        {
            Server = new Server
            {
                Services = { ProxyService.BindService(this) },
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
            };
            Server.Start();
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            return Server?.ShutdownAsync();
        }
    }
}