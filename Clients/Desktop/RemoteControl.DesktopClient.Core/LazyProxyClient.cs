using RemoteControl.Proxy;
using System.Threading.Tasks;

namespace RemoteControl.DesktopClient.Core
{
    public class LazyProxyClient
    {
        private string remoteAddress = string.Empty;
        private int remotePort;
        private ProxyClient proxyClient;

        private async Task<ProxyClient> GetProxyClient(string address, int port)
        {
            if (proxyClient == null)
            {
                proxyClient = new ProxyClient();
            }

            if (remotePort != port || remoteAddress != address)
            {
                await proxyClient.Stop();
                await proxyClient.Start(address, port);
                remotePort = port;
                remoteAddress = address;
            }
            return proxyClient;
        }
    }
}