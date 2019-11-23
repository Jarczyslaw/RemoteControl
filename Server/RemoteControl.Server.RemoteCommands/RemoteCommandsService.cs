using Grpc.Core;
using JToolbox.Desktop.Core.Services;
using RemoteControl.Proxy;
using RemoteControl.Server.Connections;
using RemoteControl.Server.Messages;
using System;
using System.Threading.Tasks;

namespace RemoteControl.Server.RemoteCommands
{
    public class RemoteCommandsService : ProxyServer, IRemoteCommandsService
    {
        private readonly ISystemService systemService;
        private readonly IConnectionsService connectionsService;
        private readonly IMessagesAggregator messagesAggregator;

        public RemoteCommandsService(IMessagesAggregator messagesAggregator, IConnectionsService connectionsService, ISystemService systemService)
        {
            this.systemService = systemService;
            this.connectionsService = connectionsService;
            this.messagesAggregator = messagesAggregator;
        }

        private void HandleInfo(ConnectionRequest request, string operationName)
        {
            messagesAggregator.Info($"{request.Name} from {request.Address} invoked {operationName}");
        }

        private void HandleError(Exception exc)
        {
            messagesAggregator.Error(exc);
        }

        public override Task<CommonResponse> Connect(ConnectionRequest request, ServerCallContext context)
        {
            var response = new CommonResponse();
            try
            {
                HandleInfo(request, nameof(Connect));
                connectionsService.HandleRequest(request);
            }
            catch (Exception exc)
            {
                HandleError(exc);
                response.Error = exc.Message;
            }
            return Task.FromResult(response);
        }

        public override Task<CommonResponse> Disconnect(ConnectionRequest request, ServerCallContext context)
        {
            var response = new CommonResponse();
            try
            {
                HandleInfo(request, nameof(Disconnect));
                connectionsService.RemoveConnection(request);
            }
            catch (Exception exc)
            {
                HandleError(exc);
                response.Error = exc.Message;
            }
            return Task.FromResult(response);
        }

        public override Task<CommonResponse> Restart(ConnectionRequest request, ServerCallContext context)
        {
            var response = new CommonResponse();
            try
            {
                HandleInfo(request, nameof(Restart));
                connectionsService.HandleRequest(request);
                systemService.Restart();
            }
            catch (Exception exc)
            {
                HandleError(exc);
                response.Error = exc.Message;
            }
            return Task.FromResult(response);
        }

        public override Task<CommonResponse> Shutdown(ConnectionRequest request, ServerCallContext context)
        {
            var response = new CommonResponse();
            try
            {
                HandleInfo(request, nameof(Shutdown));
                connectionsService.HandleRequest(request);
                systemService.Shutdown();
            }
            catch (Exception exc)
            {
                HandleError(exc);
                response.Error = exc.Message;
            }
            return Task.FromResult(response);
        }
    }
}