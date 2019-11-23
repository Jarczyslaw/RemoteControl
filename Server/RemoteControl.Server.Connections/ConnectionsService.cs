using RemoteControl.Proxy;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteControl.Server.Connections
{
    public delegate void OnNewConnection(Connection newConnection);

    public delegate void OnConnectionRemove(Connection connection);

    public delegate void OnConnectionsStatusChanged(List<Connection> connections);

    public class ConnectionsService : IConnectionsService
    {
        private static readonly object connectionsLock = new object();

        private readonly List<Connection> connections = new List<Connection>();
        private readonly CancellationTokenSource poolingTokenSource = new CancellationTokenSource();
        private readonly Task poolingTask;

        public event OnNewConnection OnNewConnection = delegate { };

        public event OnConnectionsStatusChanged OnConnectionsStatusChanged = delegate { };

        public event OnConnectionRemove OnConnectionRemove = delegate { };

        public ConnectionsService()
        {
            poolingTask = Task.Run(() => ConnectionsPooling(poolingTokenSource.Token), poolingTokenSource.Token);
        }

        public TimeSpan InactiveTime { get; set; } = TimeSpan.FromSeconds(10);

        public TimeSpan RemoveTime { get; set; } = TimeSpan.FromSeconds(30);

        private async void ConnectionsPooling(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                lock (connectionsLock)
                {
                    var connectionsStatusChanged = false;
                    for (int i = connections.Count - 1; i >= 0; i--)
                    {
                        var connection = connections[i];
                        if (DateTime.Now - connection.UpdateTime >= RemoveTime)
                        {
                            OnConnectionRemove(connection);
                            connections.RemoveAt(i);
                            connectionsStatusChanged = true;
                        }
                        else if (DateTime.Now - connection.UpdateTime >= InactiveTime)
                        {
                            connection.Active = false;
                            connectionsStatusChanged = true;
                        }
                    }

                    if (connectionsStatusChanged)
                    {
                        OnConnectionsStatusChanged(connections);
                    }
                }
                await Task.Delay(1000);
            }
        }

        public void HandleRequest(ConnectionRequest connectionRequest)
        {
            lock (connectionsLock)
            {
                var connection = connections.Find(c => c.ConnectionRequest.Equals(connectionRequest));
                if (connection == null)
                {
                    connection = new Connection
                    {
                        Active = true,
                        UpdateTime = DateTime.Now,
                        ConnectionRequest = connectionRequest
                    };
                    connections.Add(connection);
                }
                else
                {
                    connection.Active = true;
                    connection.UpdateTime = DateTime.Now;
                }
                OnNewConnection(connection);
                OnConnectionsStatusChanged(connections);
            }
        }

        public void RemoveConnection(ConnectionRequest connectionRequest)
        {
            lock (connectionsLock)
            {
                var connection = connections.Find(c => c.ConnectionRequest.Equals(connectionRequest));
                if (connection != null)
                {
                    OnConnectionRemove(connection);
                    connections.Remove(connection);
                    OnConnectionsStatusChanged(connections);
                }
            }
        }

        public void Dispose()
        {
            poolingTokenSource.Cancel();
            poolingTask.Wait();
        }
    }
}