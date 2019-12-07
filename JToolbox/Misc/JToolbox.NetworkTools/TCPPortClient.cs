using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools
{
    public class TCPPortClient : IPortClient
    {
        private readonly TcpClient client = new TcpClient();

        public Task Connect(IPAddress address, int port, int timeout)
        {
            client.ReceiveTimeout =
                client.SendTimeout = timeout;
            return client.ConnectAsync(address, port);
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}