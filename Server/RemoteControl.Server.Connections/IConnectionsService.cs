using RemoteControl.Proxy;
using System;
using System.Collections.Generic;

namespace RemoteControl.Server.Connections
{
    public interface IConnectionsService : IDisposable
    {
        event OnNewConnection OnNewConnection;

        event OnConnectionsStatusChanged OnConnectionsStatusChanged;

        event OnConnectionRemove OnConnectionRemove;

        List<Connection> Connections { get; }

        TimeSpan InactiveTime { get; set; }
        TimeSpan RemoveTime { get; set; }

        void HandleRequest(RequestBase request);

        void RemoveConnection(RequestBase request);
    }
}