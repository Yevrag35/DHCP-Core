using System;
using System.Collections.Generic;
using System.Linq;

namespace MG.Core.PowerShell.Dhcp.Models
{
    public class DhcpServerDnsCredential
    {
        public bool IsSet => !string.IsNullOrWhiteSpace(this.Domain) && !string.IsNullOrWhiteSpace(this.UserName);

        [CimProperty("DomainName")]
        public string Domain { get; private set; }

        [CimProperty]
        public string UserName { get; private set; }
    }
}