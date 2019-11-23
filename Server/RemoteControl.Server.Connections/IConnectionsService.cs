using RemoteControl.Proxy;
using System;

namespace RemoteControl.Server.Connections
{
    public interface IConnectionsService : IDisposable
    {
        event OnNewConnection OnNewConnection;

        event OnConnectionsStatusChanged OnConnectionsStatusChanged;

        event OnConnectionRemove OnConnectionRemove;

        TimeSpan InactiveTime { get; set; }
        TimeSpan RemoveTime { get; set; }

        void HandleRequest(ConnectionRequest connectionRequest);
        void RemoveConnection(ConnectionRequest connectionRequest);
    }
}