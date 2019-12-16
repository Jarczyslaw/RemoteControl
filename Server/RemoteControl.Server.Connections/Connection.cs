using RemoteControl.Proxy;
using System;

namespace RemoteControl.Server.Connections
{
    public class Connection
    {
        public RequestBase Request { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Active { get; set; }
    }
}