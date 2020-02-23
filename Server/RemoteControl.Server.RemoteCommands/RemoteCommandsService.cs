using Grpc.Core;
using JToolbox.Core.Utilities;
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

        private void HandleInfo(RequestBase request, string operationName)
        {
            messagesAggregator.Info($"{request.Name} from {request.Address} invoked {operationName}");
        }

        private void HandleError(Exception exc)
        {
            messagesAggregator.Error(exc);
        }

        public override Task<PingResponse> Ping(PingRequest request, ServerCallContext context)
        {
            var response = new PingResponse()
            {
                ResponseBase = CreateResponseBase()
            };
            ExecutionTime.Run(() => response.ResponseMessage = request.Message, out TimeSpan elapsed);
            response.ResponseBase.ExecutionTime = elapsed.Milliseconds;
            return Task.FromResult(response);
        }

        public override Task<DisconnectResponse> Disconnect(DisconnectRequest request, ServerCallContext context)
        {
            var response = new DisconnectResponse()
            {
                ResponseBase = CreateResponseBase()
            };

            ExecutionTime.Run(() =>
            {
                try
                {
                    HandleInfo(request.RequestBase, nameof(Disconnect));
                    connectionsService.RemoveConnection(request.RequestBase);
                }
                catch (Exception exc)
                {
                    HandleError(exc);
                    response.ResponseBase.Error = exc.Message;
                }
            }, out TimeSpan elapsed);

            response.ResponseBase.ExecutionTime = elapsed.Milliseconds;
            return Task.FromResult(response);
        }

        public override Task<RestartResponse> Restart(RestartRequest request, ServerCallContext context)
        {
            var response = new RestartResponse()
            {
                ResponseBase = CreateResponseBase()
            };

            ExecutionTime.Run(() =>
            {
                try
                {
                    HandleInfo(request.RequestBase, nameof(Restart));
                    connectionsService.HandleRequest(request.RequestBase);
                    systemService.Restart();
                }
                catch (Exception exc)
                {
                    HandleError(exc);
                    response.ResponseBase.Error = exc.Message;
                }
            }, out TimeSpan elapsed);

            response.ResponseBase.ExecutionTime = elapsed.Milliseconds;
            return Task.FromResult(response);
        }

        public override Task<ShutdownResponse> Shutdown(ShutdownRequest request, ServerCallContext context)
        {
            var response = new ShutdownResponse()
            {
                ResponseBase = CreateResponseBase()
            };

            ExecutionTime.Run(() =>
            {
                try
                {
                    HandleInfo(request.RequestBase, nameof(Shutdown));
                    connectionsService.HandleRequest(request.RequestBase);
                    systemService.Shutdown();
                }
                catch (Exception exc)
                {
                    HandleError(exc);
                    response.ResponseBase.Error = exc.Message;
                }
            }, out TimeSpan elapsed);

            response.ResponseBase.ExecutionTime = elapsed.Milliseconds;
            return Task.FromResult(response);
        }

        public override Task<GetSystemInformationResponse> GetSystemInformation(GetSystemInformationRequest request, ServerCallContext context)
        {
            var response = new GetSystemInformationResponse()
            {
                ResponseBase = CreateResponseBase()
            };

            ExecutionTime.Run(() =>
            {
                try
                {
                    HandleInfo(request.RequestBase, nameof(GetSystemInformation));
                    connectionsService.HandleRequest(request.RequestBase);

                    var osInfo = JToolbox.SysInformation.SystemInformation.GetOSInfo();
                    var cpuInfo = JToolbox.SysInformation.SystemInformation.GetCPUInfo();
                    var memoryInfo = JToolbox.SysInformation.SystemInformation.GetMemoryInfo();

                    response.SystemInformation = new SystemInformation
                    {
                        OSBuildNumber = osInfo.BuildNumber,
                        OSName = osInfo.Name,
                        OSVersion = osInfo.Version,
                        CPUCaption = cpuInfo.Caption,
                        CPUName = cpuInfo.Name,
                        NumberOfCores = cpuInfo.NumberOfEnabledCores,
                        NumberOfLogicalProcessors = cpuInfo.NumberOfLogicalProcessors,
                        FreeMemory = memoryInfo.FreePhysicalMemory,
                        TotalMemory = memoryInfo.TotalVisibleMemory
                    };
                }
                catch (Exception exc)
                {
                    HandleError(exc);
                    response.ResponseBase.Error = exc.Message;
                }
            }, out TimeSpan elapsed);

            response.ResponseBase.ExecutionTime = elapsed.Milliseconds;
            return Task.FromResult(response);
        }

        private ResponseBase CreateResponseBase()
        {
            return new ResponseBase
            {
                Error = string.Empty,
                ConnectionsCount = connectionsService.Connections.Count,
                ServerName = Environment.MachineName,
                ServerAddress = Address,
                ServerPort = Port.Value
            };
        }
    }
}