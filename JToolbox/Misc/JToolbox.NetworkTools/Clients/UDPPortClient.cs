using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools.Clients
{
    public class UDPPortClient : IPortClient
    {
        public Task<bool> Check(IPAddress address, int port, int timeout)
        {
            using (var client = new UdpClient())
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
                catch
                {
                    return Task.FromResult(false);
                }
                return Task.FromResult(true);
            }
        }
    }
}