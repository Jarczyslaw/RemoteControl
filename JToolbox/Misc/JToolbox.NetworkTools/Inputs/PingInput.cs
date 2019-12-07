using System.Net;

namespace JToolbox.NetworkTools.Inputs
{
    public class PingInput
    {
        public IPAddress Address { get; set; }
        public int Timeout { get; set; }
        public int Retries { get; set; }
    }
}