using System.Net;

namespace JToolbox.NetworkTools.Inputs
{
    public class PortInput
    {
        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; } = 200;
        public int Retries { get; set; } = 1;
    }
}