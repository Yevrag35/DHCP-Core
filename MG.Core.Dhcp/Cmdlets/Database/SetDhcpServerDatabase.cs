using Microsoft.Management.Infrastructure;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using MG.Core.PowerShell.Dhcp.Models;

namespace MG.Core.PowerShell.Dhcp.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "DhcpServerDatabase", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    [OutputType(typeof(DhcpServerDatabase))]
    [CmdletBinding(PositionalBinding = false)]
    public class SetDhcpServerDatabase : BaseComputerDhcpCmdlet
    {
        #region FIELDS/CONSTANTS
        private bool _passThru;

        protected override string ClassName { get; set; } = "PS_DhcpServerDatabase";
        protected override bool IsSetting => true;
        protected override string MethodName { get; set; } = "Set";

        #endregion

        #region PARAMETERS
        [Parameter(Mandatory = false)]
        public string FileName { get; set; }

        [Parameter(Mandatory = false)]
        public string BackupPath { get; set; }

        [Parameter(Mandatory = false)]
        public uint BackupInterval { get; set; }

        [Parameter(Mandatory = false)]
        public uint CleanupInterval { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter RestoreFromBackup { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru
        {
            get => _passThru;
            set => _passThru = value;
        }

        #endregion

        #region CMDLET PROCESSING
        protected override void BeginProcessing() => base.BeginProcessing();

        protected override void ProcessRecord()
        {
            base.AddParameters(this, x => x.BackupInterval, x => x.BackupPath, x => x.CleanupInterval,
                x => x.FileName, x => x.RestoreFromBackup);

            base.AddParameter(this, x => x.PassThru, true);

            IEnumerable<CimMethodResult> results = base.ExecuteStaticMethod();
            base.WriteResults<DhcpServerDatabase>(results, _passThru);
        }

        #endregion
    }
}