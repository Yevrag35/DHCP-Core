using MG.Core.PowerShell.Dhcp.Converters;
using Microsoft.Management.Infrastructure;
using System;
using System.Collections.Generic;

namespace MG.Core.PowerShell.Dhcp.Models
{
    public class DhcpAuditLog : BaseResult
    {
        [CimProperty]
        public uint DiskCheckInterval { get; private set; }

        [CimProperty("Enable")]
        public bool Enabled { get; private set; }

        [CimProperty]
        public uint MaxMBFileSize { get; private set; }

        [CimProperty]
        public uint MinMBDiskSpace { get; private set; }

        [CimProperty]
        public string Path { get; private set; }
    }
}
