using Grpc.Core;
using JToolbox.Desktop.Core.Services;
using RemoteControl.Proxy;
using System.Threading.Tasks;

namespace RemoteControl.Server.RemoteCommands
{
    public class RemoteCommandsService : ProxyService.ProxyServiceBase, IRemoteCommandsService
    {
        private readonly ISystemService systemService;
        private Grpc.Core.Server server;

        public RemoteCommandsService(ISystemService systemService)
        {
            this.systemService = systemService;
        }

        public Task Start(int port)
        {
            server = new Grpc.Core.Server
            {
                Services = { ProxyService.BindService(this) },
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
            };
            server.Start();
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            return server?.ShutdownAsync();
        }

        public override Task<ConnectResponse> Connect(ConnectionRequest request, ServerCallContext context)
        {
            return base.Connect(request, context);
        }

        public override Task<DisconnectResponse> Disconnect(ConnectionRequest request, ServerCallContext context)
        {
            return base.Disconnect(request, context);
        }

        public override Task<RestartResponse> Restart(ConnectionRequest request, ServerCallContext context)
        {
            return base.Restart(request, context);
        }

        public override Task<ShutdownResponse> Shutdown(ConnectionRequest request, ServerCallContext context)
        {
            return base.Shutdown(request, context);
        }
    }
}