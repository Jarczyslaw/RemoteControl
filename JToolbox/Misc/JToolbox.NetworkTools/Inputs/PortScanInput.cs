using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace JToolbox.NetworkTools.Inputs
{
    public class PortScanInput
    {
        public List<IPAddress> Addresses { get; set; }
        public List<int> Ports { get; set; }
        public int Workers { get; set; } = Environment.ProcessorCount;
        public int Timeout { get; set; } = 200;
        public int Retries { get; set; } = 1;
        public CancellationToken CancellationToken { get; set; } = CancellationToken.None;
    }
}