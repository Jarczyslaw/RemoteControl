using System;
using System.Net;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools
{
    public interface IPortClient : IDisposable
    {
        Task Connect(IPAddress address, int port, int timeout);
    }
}