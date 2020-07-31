using MG.Core.PowerShell.Dhcp.Models;
using Microsoft.Management.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace MG.Core.PowerShell.Dhcp.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "DhcpServerDnsCredential")]
    [OutputType(typeof(DhcpServerDnsCredential))]
    public class GetDhcpServerDnsCredential : BaseComputerDhcpCmdlet
    {
        #region PROPERTIES/FIELDS
        protected override string ClassName { get; set; } = "PS_DhcpServerDnsCredential";
        protected override bool IsSetting => false;
        protected override string MethodName { get; set; } = "Get";

        #endregion

        #region CMDLET PROCESSING
        protected override void BeginProcessing() => base.BeginProcessing();

        protected override void ProcessRecord()
        {
            base.WriteResults<DhcpServerDnsCredential>(base.ExecuteStaticMethod());
        }

        #endregion
    }
}