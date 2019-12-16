using System.Threading.Tasks;

namespace RemoteControl.Proxy
{
    public class LazyProxyClient
    {
        private ProxyClient proxyClient;

        public string RemoteAddress { get; private set; }
        public int RemotePort { get; private set; }

        public async Task<ProxyClient> GetProxyClient(string address, int port)
        {
            if (proxyClient == null)
            {
                proxyClient = new ProxyClient();
            }

            if (RemotePort != port || RemoteAddress != address)
            {
                await proxyClient.Stop();
                await proxyClient.Start(address, port);
                RemotePort = port;
                RemoteAddress = address;
            }
            return proxyClient;
        }
    }
}