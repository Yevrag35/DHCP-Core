using System;
using System.Collections.Generic;
using System.Linq;

namespace MG.Core.PowerShell.Dhcp.Models
{
    public class DhcpServerDnsCredential
    {
        [CimProperty("DomainName")]
        public string Domain { get; private set; }

        [CimProperty]
        public string UserName { get; private set; }
    }
}