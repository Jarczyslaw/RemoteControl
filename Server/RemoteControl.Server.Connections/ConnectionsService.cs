using RemoteControl.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly CancellationTokenSource poolingTokenSource = new CancellationTokenSource();
        private readonly Task poolingTask;

        public event OnNewConnection OnNewConnection = delegate { };

        public event OnConnectionsStatusChanged OnConnectionsStatusChanged = delegate { };

        public event OnConnectionRemove OnConnectionRemove = delegate { };

        public ConnectionsService()
        {
            poolingTask = Task.Run(() => ConnectionsPooling(poolingTokenSource.Token), poolingTokenSource.Token);
        }

        public List<Connection> Connections { get; } = new List<Connection>();

        public TimeSpan InactiveTime { get; set; } = TimeSpan.FromSeconds(10);

        public TimeSpan RemoveTime { get; set; } = TimeSpan.FromSeconds(30);

        private async void ConnectionsPooling(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var connectionsStatusChanged = false;
                Connection removedConnection = null;
                List<Connection> connectionsCopy = null;

                lock (connectionsLock)
                {
                    for (int i = Connections.Count - 1; i >= 0; i--)
                    {
                        var connection = Connections[i];
                        if (DateTime.Now - connection.UpdateTime >= RemoveTime)
                        {
                            removedConnection = connection;
                            Connections.RemoveAt(i);
                            connectionsStatusChanged = true;
                        }
                        else if (DateTime.Now - connection.UpdateTime >= InactiveTime)
                        {
                            connection.Active = false;
                            connectionsStatusChanged = true;
                        }
                    }
                    connectionsCopy = Connections.ToList();
                }

                if (removedConnection != null)
                {
                    OnConnectionRemove(removedConnection);
                }

                if (connectionsStatusChanged)
                {
                    OnConnectionsStatusChanged(connectionsCopy);
                }

                await Task.Delay(1000);
            }
        }

        public void HandleRequest(ConnectionRequest connectionRequest)
        {
            Connection newConnection = null;
            List<Connection> connectionsCopy = null;

            lock (connectionsLock)
            {
                var connection = Connections.Find(c => c.ConnectionRequest.Equals(connectionRequest));
                if (connection == null)
                {
                    newConnection = new Connection
                    {
                        Active = true,
                        UpdateTime = DateTime.Now,
                        ConnectionRequest = connectionRequest
                    };
                    Connections.Add(newConnection);
                }
                else
                {
                    connection.Active = true;
                    connection.UpdateTime = DateTime.Now;
                }

                connectionsCopy = Connections.ToList();
            }

            if (newConnection != null)
            {
                OnNewConnection(newConnection);
            }

            OnConnectionsStatusChanged(connectionsCopy);
        }

        public void RemoveConnection(ConnectionRequest connectionRequest)
        {
            Connection removedConnection = null;
            List<Connection> connectionsCopy = null;

            lock (connectionsLock)
            {
                removedConnection = Connections.Find(c => c.ConnectionRequest.Equals(connectionRequest));
                if (removedConnection != null)
                {
                    Connections.Remove(removedConnection);
                }
                connectionsCopy = Connections.ToList();
            }

            if (removedConnection != null)
            {
                OnConnectionRemove(removedConnection);
                OnConnectionsStatusChanged(connectionsCopy);
            }
        }

        public void Dispose()
        {
            poolingTokenSource.Cancel();
            poolingTask.Wait();
        }
    }
}