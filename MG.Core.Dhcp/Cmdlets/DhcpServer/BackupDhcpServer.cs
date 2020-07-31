using System;
using System.Linq;
using System.Management.Automation;

namespace MG.Core.PowerShell.Dhcp.Cmdlets
{
    [Cmdlet(VerbsData.Backup, "DhcpServer", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    public class BackupDhcpServer : DhcpServerCmdlet
    {
        #region FIELDS/CONSTANTS
        protected override bool IsSetting => false;
        protected override string MethodName { get; set; } = "Backup";

        #endregion

        #region CMDLET PROCESSING
        protected override void BeginProcessing() => base.BeginProcessing();
        protected override void ProcessRecord() => base.ProcessRecord();

        #endregion
    }
}