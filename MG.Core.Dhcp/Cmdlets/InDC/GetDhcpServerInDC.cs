using MG.Core.PowerShell.Dhcp.Models;
using System;
using System.Management.Automation;

namespace MG.Core.PowerShell.Dhcp.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "DhcpServerInDC")]
    public class GetDhcpServerInDC : BaseDhcpCmdlet
    {
        protected override string ClassName { get; set; } = "PS_DhcpServerInDC";
        protected override bool IsSetting => false;
        protected override string MethodName { get; set; } = "Get";

        protected override void BeginProcessing() => base.BeginProcessing();

        protected override void ProcessRecord()
        {
            base.WriteResults<DhcpServer>(ExecuteStaticMethod());
        }
    }
}
