using System;
using System.Net;
using System.Threading;

namespace JToolbox.NetworkTools.Inputs
{
    public class PortScanInput
    {
        public IPAddress Address { get; set; }
        public int Workers { get; set; } = Environment.ProcessorCount;
        public int StartPort { get; set; }
        public int EndPort { get; set; }
        public int Timeout { get; set; } = 200;
        public int Retries { get; set; } = 1;
        public CancellationToken CancellationToken { get; set; } = CancellationToken.None;
    }
}