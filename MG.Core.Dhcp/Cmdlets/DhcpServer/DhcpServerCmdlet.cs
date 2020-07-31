using MG.Posh.Extensions.Bound;
using Microsoft.Management.Infrastructure;
using System;
using System.Linq;
using System.Management.Automation;

namespace MG.Core.PowerShell.Dhcp.Cmdlets
{
    public abstract class DhcpServerCmdlet : BaseComputerDhcpCmdlet
    {
        protected bool _isForcing;
        protected override string ClassName { get; set; } = "PS_DhcpServer";

        #region PARAMETERS
        [Parameter(Mandatory = false, Position = 0)]
        public string Path { get; set; }

        #endregion

        #region CMDLET PROCESSING
        protected override void BeginProcessing() => base.BeginProcessing();

        protected override void ProcessRecord()
        {
            if (_isForcing || base.DefaultShouldProcess())
            {
                if (this.ContainsParameter(x => x.Path))
                    base.AddParameters(this, x => x.Path);

                foreach (CimMethodResult result in base.ExecuteStaticMethod())
                {
                    if (result?.ReturnValue != null && result?.ReturnValue?.Value != null && (uint)result.ReturnValue.Value != 0)
                        base.WriteError((uint)result.ReturnValue.Value);
                }
            }
        }

        #endregion
    }
}