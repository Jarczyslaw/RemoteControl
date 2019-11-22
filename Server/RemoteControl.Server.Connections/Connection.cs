using RemoteControl.Proxy;
using System;

namespace RemoteControl.Server.Connections
{
    public class Connection
    {
        public ConnectionRequest ConnectionRequest { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool Active { get; set; }
    }
}