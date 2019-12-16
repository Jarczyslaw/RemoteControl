using RemoteControl.Server.Connections;
using System;
using static RemoteControl.Proxy.RequestBase.Types;

namespace RemoteControl.Server.Core.ViewModels
{
    public class ConnectionViewModel
    {
        private readonly Connection connection;

        public ConnectionViewModel(Connection connection)
        {
            this.connection = connection;
        }

        public string Name => connection.Request.Name;
        public string Address => connection.Request.Address;
        public DateTime UpdateTime => connection.UpdateTime;
        public bool Active => connection.Active;
        public DeviceType Type => connection.Request.Type;
    }
}