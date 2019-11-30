using Grpc.Core;
using System.Threading.Tasks;

namespace RemoteControl.Proxy
{
    public delegate void OnStart(string address, int port);

    public delegate void OnStop();

    public class ProxyServer : ProxyService.ProxyServiceBase, IProxyServer
    {
        public event OnStart OnStart = delegate { };

        public event OnStop OnStop = delegate { };

        public Server Server { get; private set; }

        public Task Start(string address, int port)
        {
            Server = new Server
            {
                Services = { ProxyService.BindService(this) },
                Ports = { new ServerPort(address, port, ServerCredentials.Insecure) }
            };
            Server.Start();
            OnStart(address, port);
            return Task.CompletedTask;
        }

        public async Task Stop()
        {
            await Server?.ShutdownAsync();
            OnStop();
        }
    }
}