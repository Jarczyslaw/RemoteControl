﻿using JToolbox.Core.Extensions;
using JToolbox.Core.Helpers;
using JToolbox.NetworkTools.Inputs;
using JToolbox.NetworkTools.Results;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools
{
    public delegate void OnDeviceScanned(PingResult result);

    public delegate void OnDevicesScanComplete(List<PingResult> results);

    public class PingScanner
    {
        public event OnDeviceScanned OnDeviceScanned = delegate { };

        public event OnDevicesScanComplete OnDevicesScanComplete = delegate { };

        public async Task<List<PingResult>> PingScan(PingScanInput pingScanInput)
        {
            var result = new BlockingCollection<PingResult>();
            var addressesPacks = pingScanInput.Addresses.ChunkInto(pingScanInput.Workers);

            await AsyncHelper.ForEach(addressesPacks, async (addressPack, token) =>
            {
                foreach (var address in addressPack)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                    var pingResult = await Ping(new PingInput
                    {
                        Address = address,
                        Retries = pingScanInput.Retries,
                        Timeout = pingScanInput.Timeout
                    });
                    result.Add(pingResult);
                    OnDeviceScanned(pingResult);
                }
            }, pingScanInput.CancellationToken);
            var finalResult = result.ToList();
            OnDevicesScanComplete(finalResult);
            return finalResult;
        }

        public async Task<PingResult> Ping(PingInput pingInput)
        {
            using (var ping = new Ping())
            {
                PingReply pingReply = null;
                Exception exception = null;
                for (int i = 0; i < pingInput.Retries; i++)
                {
                    try
                    {
                        pingReply = await ping.SendPingAsync(pingInput.Address, pingInput.Timeout);
                        if (pingReply.Status == IPStatus.Success)
                        {
                            break;
                        }
                    }
                    catch (Exception exc)
                    {
                        exception = exc;
                    }
                }
                return new PingResult
                {
                    Address = pingInput.Address,
                    Reply = pingReply,
                    LastException = exception
                };
            }
        }
    }
}