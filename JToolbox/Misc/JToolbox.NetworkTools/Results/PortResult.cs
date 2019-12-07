using System.Net;

namespace JToolbox.NetworkTools.Results
{
    public class PortResult
    {
        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public PortType Type { get; set; }
        public bool IsOpen { get; set; }
    }
}