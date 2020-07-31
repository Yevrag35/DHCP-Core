using System;
using System.Collections.Generic;
using System.Text;

namespace MG.Core.PowerShell.Dhcp.Cmdlets.AuditLog
{
    public abstract class DhcpAuditLogCmdlet : BaseComputerDhcpCmdlet
    {
        protected override string ClassName { get; set; } = "PS_DhcpServerAuditLog";
        

        protected override void BeginProcessing() => base.BeginProcessing();
    }
}
