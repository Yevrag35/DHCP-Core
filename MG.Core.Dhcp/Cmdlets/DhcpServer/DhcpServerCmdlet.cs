using MG.Posh.Extensions.Shoulds;
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
        protected override bool IsSetting => false;

        #region PARAMETERS
        [Parameter(Mandatory = true, Position = 0)]
        public string Path { get; set; }

        #endregion

        #region CMDLET PROCESSING
        protected override void BeginProcessing() => base.BeginProcessing();

        protected override void ProcessRecord()
        {
            if (_isForcing || base.DefaultShouldProcess())
            {
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