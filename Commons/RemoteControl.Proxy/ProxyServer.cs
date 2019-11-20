using Grpc.Core;
using System.Threading.Tasks;

namespace RemoteControl.Proxy
{
    public delegate void OnStart(int port);

    public delegate void OnStop();

    public class ProxyServer : ProxyService.ProxyServiceBase, IProxyServer
    {
        public event OnStart OnStart = delegate { };

        public event OnStop OnStop = delegate { };

        public Server Server { get; private set; }

        public Task Start(int port)
        {
            Server = new Server
            {
                Services = { ProxyService.BindService(this) },
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
            };
            Server.Start();
            OnStart(port);
            return Task.CompletedTask;
        }

        public async Task Stop()
        {
            await Server?.ShutdownAsync();
            OnStop();
        }
    }
}