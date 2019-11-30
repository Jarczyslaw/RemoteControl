using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace JToolbox.Core.Utilities
{
    public static class NetworkUtils
    {
        public static IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return Array.Find(host.AddressList, a => a.AddressFamily == AddressFamily.InterNetwork);
        }

        public static List<IPAddress> GetLocalIPAddresses()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork)
                .ToList();
        }

        public static bool ConnectedToLocalNetwork()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }
    }
}