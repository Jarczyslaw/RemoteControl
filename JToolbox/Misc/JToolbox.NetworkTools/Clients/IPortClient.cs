using System;
using System.Net;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools.Clients
{
    public interface IPortClient
    {
        Task<bool> Check(IPAddress address, int port, int timeout);
    }
}