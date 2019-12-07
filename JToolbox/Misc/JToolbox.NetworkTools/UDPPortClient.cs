using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools
{
    public class UDPPortClient : IPortClient
    {
        private readonly UdpClient client = new UdpClient();

        public Task Connect(IPAddress address, int port, int timeout)
        {
            client.Client.ReceiveTimeout =
                client.Client.SendTimeout = timeout;
            client.Connect(address, port);
            try
            {
                client.Send(new byte[] { 0 }, 1);
                IPEndPoint endPoint = null;
                client.Receive(ref endPoint);
            }
            catch (TimeoutException) { }
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}