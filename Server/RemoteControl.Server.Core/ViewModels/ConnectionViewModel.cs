using RemoteControl.Server.Connections;
using System;

namespace RemoteControl.Server.Core.ViewModels
{
    public class ConnectionViewModel
    {
        private readonly Connection connection;

        public ConnectionViewModel(Connection connection)
        {
            this.connection = connection;
        }

        public string Name => connection.ConnectionRequest.Name;
        public string Address => connection.ConnectionRequest.Address;
        public DateTime UpdateTime => connection.UpdateTime;
        public bool Active => connection.Active;
        public Proxy.ConnectionRequest.Types.DeviceType Type => connection.ConnectionRequest.Type;
    }
}