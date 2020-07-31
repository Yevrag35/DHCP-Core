using Microsoft.Management.Infrastructure;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using MG.Core.PowerShell.Dhcp.Models;

namespace MG.Core.PowerShell.Dhcp.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "DhcpServerDatabase")]
    [OutputType(typeof(DhcpServerDatabase))]
    public class GetDhcpServerDatabase : BaseComputerDhcpCmdlet
    {
        #region FIELDS/CONSTANTS
        protected override string ClassName { get; set; } = "PS_DhcpServerDatabase";
        protected override bool IsSetting => false;
        protected override string MethodName { get; set; } = "Get";

        #endregion

        #region CMDLET PROCESSING
        protected override void BeginProcessing() => base.BeginProcessing();

        protected override void ProcessRecord()
        {
            IEnumerable<CimMethodResult> results = base.ExecuteStaticMethod();
            base.WriteResults<DhcpServerDatabase>(results);
        }

        #endregion
    }
}