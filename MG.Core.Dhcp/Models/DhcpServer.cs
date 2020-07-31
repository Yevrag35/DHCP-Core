using MG.Core.PowerShell.Dhcp.Converters;
using Microsoft.Management.Infrastructure;
using System;
using System.Net;

namespace MG.Core.PowerShell.Dhcp.Models
{
    public class DhcpServer// : BaseResult
    {
        [CimProperty]
        public string DnsName { get; private set; }

        [CimProperty(typeof(StringToIP))]
        public IPAddress IPAddress { get; private set; }
    }
}
