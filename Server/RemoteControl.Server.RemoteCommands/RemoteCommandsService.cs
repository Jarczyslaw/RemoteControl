using Grpc.Core;
using JToolbox.Desktop.Core.Services;
using RemoteControl.Proxy;
using System.Threading.Tasks;

namespace RemoteControl.Server.RemoteCommands
{
    public class RemoteCommandsService : ProxyServer, IRemoteCommandsService
    {
        private readonly ISystemService systemService;

        public RemoteCommandsService(ISystemService systemService)
        {
            this.systemService = systemService;
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