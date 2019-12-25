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

        public bool IsListening { get; private set; }

        public int? Port { get; private set; }

        public string Address { get; private set; }

        public async Task Start(string address, int port)
        {
            try
            {
                await Stop();
                Server = new Server
                {
                    Services = { ProxyService.BindService(this) },
                    Ports = { new ServerPort(address, port, ServerCredentials.Insecure) }
                };
                Server.Start();
                SetListening(address, port);
                OnStart(address, port);
            }
            catch
            {
                Server = null;
                ResetListening();
                throw;
            }
        }

        private void SetListening(string address, int port)
        {
            IsListening = true;
            Address = address;
            Port = port;
        }

        private void ResetListening()
        {
            IsListening = false;
            Address = null;
            Port = null;
        }

        public async Task Stop()
        {
            if (Server != null)
            {
                await Server.ShutdownAsync();
                ResetListening();
                OnStop();
            }
        }
    }
}