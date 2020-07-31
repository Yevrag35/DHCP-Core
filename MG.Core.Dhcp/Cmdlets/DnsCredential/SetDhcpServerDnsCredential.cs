using MG.Core.PowerShell.Dhcp.Models;
using MG.Posh.Extensions.Bound;
using Microsoft.Management.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Xml;

namespace MG.Core.PowerShell.Dhcp.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "DhcpServerDnsCredential", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public class SetDhcpServerDnsCredential : BaseComputerDhcpCmdlet
    {
        #region PROPERTIES/FIELDS
        protected override string ClassName { get; set; } = "PS_DhcpServerDnsCredential";
        protected override bool IsSetting => true;
        protected override string MethodName { get; set; } = "Set";

        #endregion

        #region PARAMETERS
        [Parameter(Mandatory = true, Position = 0)]
        public PSCredential Credential { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }

        #endregion

        #region CMDLET PROCESSING
        protected override void BeginProcessing() => base.BeginProcessing();

        protected override void ProcessRecord()
        {
            var initial = InitialSessionState.CreateDefault();
            initial.ImportPSModule("DhcpServer");
            var powershell = System.Management.Automation.PowerShell.Create(initial).AddCommand("Set-DhcpServerDnsCredential")
                .AddParameter("Credential", this.Credential)
                .AddParameter("ComputerName", this.ComputerName);

            if (this.ContainsParameter(x => x.CimSession))
            {
                powershell = powershell.AddParameter("CimSession", this.CimSession);
            }

            if (this.ContainsParameter(x => x.PassThru))
            {
                powershell = powershell.AddParameter("PassThru", this.PassThru);
            }

            using (powershell)
            {
                Collection<PSObject> results = powershell.Invoke();
                base.WriteObject(results, true);
                powershell.Commands.Clear();
                powershell.AddScript("Get-PSSession | Remove-PSSession");
                powershell.Invoke();
                powershell.Commands.Clear();
                powershell.Streams.ClearStreams();
                base.WriteDebug("Disposed of Compatibility Session.");
            }
            GC.Collect();
        }

        #endregion
    }
}