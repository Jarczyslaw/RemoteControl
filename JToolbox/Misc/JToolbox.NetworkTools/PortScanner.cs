using JToolbox.Core.Extensions;
using JToolbox.Core.Helpers;
using JToolbox.NetworkTools.Clients;
using JToolbox.NetworkTools.Inputs;
using JToolbox.NetworkTools.Results;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools
{
    public delegate void OnPortScanned(IPAddress address, PortResult result);

    public delegate void OnPortsScanComplete(List<PortScanResult> results);

    public class PortScanner
    {
        public event OnPortScanned OnPortScanned = delegate { };

        public event OnPortsScanComplete OnPortsScanComplete = delegate { };

        public async Task<List<PortScanResult>> PortScan(PortScanInput portScanInput, IPortClient portClient)
        {
            var result = new BlockingCollection<PortScanResult>();
            var mergedInput = MergePortScanInput(portScanInput);
            var addressPortPairs = mergedInput.ChunkInto(portScanInput.Workers);

            await AsyncHelper.ForEach(addressPortPairs, async (addressPortPair, token) =>
            {
                foreach (var pair in addressPortPair)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                    var portResultEntry = await IsPortOpen(new PortInput
                    {
                        Address = pair.Address,
                        Retries = portScanInput.Retries,
                        Timeout = portScanInput.Timeout,
                        Port = pair.Port
                    }, portClient);

                    var addressResult = result.FirstOrDefault(r => r.Address == pair.Address);
                    if (addressResult == null)
                    {
                        var portResult = new PortScanResult
                        {
                            Address = pair.Address,
                        };
                        portResult.Results.Add(portResultEntry);
                        result.Add(portResult);
                    }
                    else
                    {
                        addressResult.Results.Add(portResultEntry);
                    }
                    OnPortScanned(pair.Address, portResultEntry);
                }
            }, portScanInput.CancellationToken);
            var finalResult = result.ToList();
            OnPortsScanComplete(finalResult);
            return finalResult;
        }

        private List<AddressPortPair> MergePortScanInput(PortScanInput portScanInput)
        {
            var result = new List<AddressPortPair>();
            foreach (var address in portScanInput.Addresses)
            {
                foreach (var port in portScanInput.Ports)
                {
                    result.Add(new AddressPortPair
                    {
                        Address = address,
                        Port = port
                    });
                }
            }
            return result;
        }

        public async Task<PortResult> IsPortOpen(PortInput input, IPortClient portClient)
        {
            var isOpen = false;
            Exception exception = null;
            for (int i = 0; i < input.Retries; i++)
            {
                try
                {
                    isOpen = await portClient.Check(input.Address, input.Port, input.Timeout);
                    if (isOpen)
                    {
                        break;
                    }
                }
                catch (Exception exc)
                {
                    exception = exc;
                }
            }

            return new PortResult
            {
                IsOpen = isOpen,
                Port = input.Port,
                LastException = exception
            };
        }
    }
}