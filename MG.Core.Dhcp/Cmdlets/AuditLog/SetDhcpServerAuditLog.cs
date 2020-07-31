using MG.Core.PowerShell.Dhcp.Cmdlets.AuditLog;
using MG.Core.PowerShell.Dhcp.Models;
using MG.Posh.Extensions.Shoulds;
using System;
using System.Management.Automation;

namespace MG.Core.PowerShell.Dhcp.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "DhcpServerAuditLog", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    [OutputType(typeof(DhcpAuditLog))]
    public class SetDhcpServerAuditLog : DhcpAuditLogCmdlet
    {
        private bool _force;

        protected override bool IsSetting => true;
        protected override string MethodName { get; set; } = "Set";

        [Parameter(Mandatory = false)]
        public uint DiskCheckInterval { get; set; }

        [Parameter(Mandatory = false)]
        [Alias("Enabled")]
        public SwitchParameter Enable { get; set; }

        [Parameter(Mandatory = false)]
        public uint MaxMBFileSize { get; set; }

        [Parameter(Mandatory = false)]
        public uint MinMBDiskSpace { get; set; }

        [Parameter(Mandatory = false)]
        public string Path { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Force
        {
            get => _force;
            set => _force = value;
        }

        protected override void BeginProcessing() => base.BeginProcessing();
        protected override void ProcessRecord()
        {
            this.AddParameters(this, x => x.DiskCheckInterval, x => x.Enable,
                x => x.MaxMBFileSize, x => x.MinMBDiskSpace,
                x => x.Path);

            this.AddParameter(this, x => x.PassThru, true);

            if (_force || this.ShouldProcessFormat("Set", "DhcpServer"))
            {
                base.WriteResults<DhcpAuditLog>(base.ExecuteStaticMethod(), this.PassThru.ToBool());
            }
        }
    }
}
