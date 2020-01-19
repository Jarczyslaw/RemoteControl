using System;

namespace JToolbox.NetworkTools.Results
{
    public class PortResult
    {
        public int Port { get; internal set; }
        public bool IsOpen { get; internal set; }
        public Exception LastException { get; internal set; }
    }
}