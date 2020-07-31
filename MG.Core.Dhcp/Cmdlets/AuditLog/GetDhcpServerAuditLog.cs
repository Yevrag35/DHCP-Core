using MG.Core.PowerShell.Dhcp.Models;
using Microsoft.Management.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace MG.Core.PowerShell.Dhcp.Cmdlets.AuditLog
{
    [Cmdlet(VerbsCommon.Get, "DhcpServerAuditLog")]
    public class GetDhcpServerAuditLog : DhcpAuditLogCmdlet
    {
        protected override bool IsSetting => false;
        protected override string MethodName { get; set; } = "Get";

        protected override void BeginProcessing() => base.BeginProcessing();

        protected override void ProcessRecord()
        {
            base.WriteResults<DhcpAuditLog>(base.ExecuteStaticMethod());
        }
    }
}
