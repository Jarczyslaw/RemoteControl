using System.Collections.Generic;
using System.Net;

namespace JToolbox.NetworkTools.Results
{
    public class PortScanResult
    {
        public IPAddress Address { get; internal set; }
        public List<PortResult> Results { get; internal set; } = new List<PortResult>();
    }
}