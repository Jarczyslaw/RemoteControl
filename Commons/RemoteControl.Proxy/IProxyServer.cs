﻿using Grpc.Core;
using System.Threading.Tasks;

namespace RemoteControl.Proxy
{
    public interface IProxyServer
    {
        Server Server { get; }

        Task Start(string address, int port);

        Task Stop();
    }
}