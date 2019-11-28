using Grpc.Core;
using System.Threading.Tasks;

namespace RemoteControl.Proxy
{
    public class ProxyClient : IProxyClient
    {
        public Channel Channel { get; private set; }
        public ProxyService.ProxyServiceClient Client { get; private set; }

        public Task Start(string address, int port)
        {
            Channel = new Channel($"{address}:{port}", ChannelCredentials.Insecure);
            Client = new ProxyService.ProxyServiceClient(Channel);
            return Task.CompletedTask;
        }

        public async Task Stop()
        {
            if (Client != null && Channel != null)
            {
                await Channel.ShutdownAsync();
            }
        }
    }
}