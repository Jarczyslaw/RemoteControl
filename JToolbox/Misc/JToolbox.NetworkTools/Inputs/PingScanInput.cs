using System;
using System.Net;
using System.Threading;

namespace JToolbox.NetworkTools.Inputs
{
    public class PingScanInput
    {
        public IPAddress StartAddress { get; set; }
        public IPAddress EndAddress { get; set; }
        public int Workers { get; set; } = Environment.ProcessorCount;
        public int Timeout { get; set; } = 100;
        public int Retries { get; set; } = 1;
        public CancellationToken CancellationToken { get; set; } = CancellationToken.None;
    }
}