using JToolbox.Core.Extensions;
using JToolbox.Core.Helpers;
using JToolbox.NetworkTools.Inputs;
using JToolbox.NetworkTools.Results;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools
{
    public delegate void OnPortScanned(PortResult result);

    public delegate void OnPortsScanComplete(List<PortResult> results);

    public class PortScanner
    {
        public event OnPortScanned OnPortScanned = delegate { };

        public event OnPortsScanComplete OnPortsScanComplete = delegate { };

        private readonly PortScannerFactory portScannerFactory = new PortScannerFactory();

        public async Task<List<PortResult>> PortScan(PortScanInput portScanInput, PortType portType = PortType.TCP)
        {
            var result = new BlockingCollection<PortResult>();
            var portsRange = Enumerable.Range(portScanInput.StartPort, portScanInput.EndPort - portScanInput.StartPort + 1)
                .ToList();
            var portsPacks = portsRange.ChunkInto(portScanInput.Workers);

            await AsyncHelper.ForEach(portsPacks, async (portsPack, token) =>
            {
                foreach (var port in portsPack)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                    var portResult = await IsPortOpen(new PortInput
                    {
                        Address = portScanInput.Address,
                        Retries = portScanInput.Retries,
                        Timeout = portScanInput.Timeout,
                        Port = port
                    }, portType);
                    result.Add(portResult);
                    OnPortScanned(portResult);
                }
            }, portScanInput.CancellationToken);
            var finalResult = result.ToList();
            OnPortsScanComplete(finalResult);
            return finalResult;
        }

        public async Task<PortResult> IsPortOpen(PortInput input, PortType portType = PortType.TCP)
        {
            var isOpen = false;
            using (var client = portScannerFactory.GetClient(portType))
            {
                for (int i = 0; i < input.Retries; i++)
                {
                    try
                    {
                        await client.Connect(input.Address, input.Port, input.Timeout);
                        isOpen = true;
                        break;
                    }
                    catch { }
                }
            }

            return new PortResult
            {
                IsOpen = isOpen,
                Address = input.Address,
                Port = input.Port,
                Type = portType
            };
        }
    }
}