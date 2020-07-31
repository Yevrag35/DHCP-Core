using System;
using System.Management.Automation;

namespace MG.Core.PowerShell.Dhcp.Cmdlets
{
    [Cmdlet(VerbsData.Restore, "DhcpServer", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public class RestoreDhcpServer : DhcpServerCmdlet
    {
        #region FIELDS/CONSTANTS
        protected override bool IsSetting => true;
        protected override string MethodName { get; set; } = "Restore";

        #endregion

        #region PARAMETERS
        [Parameter(Mandatory = false)]
        public SwitchParameter Force
        {
            get => _isForcing;
            set => _isForcing = value;
        }

        #endregion

        #region CMDLET PROCESSING
        protected override void BeginProcessing() => base.BeginProcessing();

        protected override void ProcessRecord()
        {
            base.AddParameter(this, x => x.Force, true);
            base.ProcessRecord();
        }
        protected override void EndProcessing()
        {
            base.WriteWarning("Please restart the DHCP server for the restored database to take effect.");
        }

        #endregion
    }
}