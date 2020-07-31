using System;

namespace MG.Core.PowerShell.Dhcp.Models
{
    public class DhcpServerDatabase : BaseResult
    {
        [CimProperty]
        public uint BackupInterval { get; set; }

        [CimProperty]
        public string BackupPath { get; set; }

        [CimProperty]
        public uint CleanupInterval { get; set; }

        [CimProperty]
        public string FileName { get; set; }

        [CimProperty]
        public bool LoggingEnabled { get; set; }

        [CimProperty]
        public bool RestoreFromBackup { get; set; }
    }
}
