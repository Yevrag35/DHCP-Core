using Microsoft.Management.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace MG.Core.PowerShell.Dhcp.Cmdlets
{
    public abstract class BaseComputerDhcpCmdlet : BaseDhcpCmdlet
    {
        [Parameter(Mandatory = false)]
        public string ComputerName { get; set; } = Environment.MachineName;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            this.AddComputerParameter();
        }
        protected override void ProcessRecord() => base.ProcessRecord();
        protected override void EndProcessing() => base.EndProcessing();

        private void AddComputerParameter() => base.AddParameters(this, x => x.ComputerName);
    }
}
