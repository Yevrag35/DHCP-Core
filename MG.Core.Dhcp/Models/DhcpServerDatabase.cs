using System;

namespace MG.Core.PowerShell.Dhcp.Models
{
    public class DhcpServerDatabase : BaseResult
    {
        [CimProperty]
        public uint BackupInterval { get; private set; }

        [CimProperty]
        public string BackupPath { get; private set; }

        [CimProperty]
        public uint CleanupInterval { get; private set; }

        [CimProperty]
        public string FileName { get; private set; }

        [CimProperty]
        public bool LoggingEnabled { get; private set; }

        [CimProperty]
        public bool RestoreFromBackup { get; private set; }
    }
}
