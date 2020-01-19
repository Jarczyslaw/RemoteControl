using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools.Clients
{
    public class TCPPortClient : IPortClient
    {
        public async Task<bool> Check(IPAddress address, int port, int timeout)
        {
            using (var client = new TcpClient())
            {
                client.ReceiveTimeout =
                    client.SendTimeout = timeout;
                await client.ConnectAsync(address, port);
                return true;
            }
        }
    }
}