using System.Threading.Tasks;

namespace RemoteControl.Proxy
{
    public interface ILazyProxyClient
    {
        string RemoteAddress { get; }
        int RemotePort { get; }

        Task Disconnect();
        Task<ProxyClient> GetProxyClient(string address, int port);
    }
}