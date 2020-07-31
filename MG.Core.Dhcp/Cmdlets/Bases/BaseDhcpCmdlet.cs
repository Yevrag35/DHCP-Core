using MG.Posh.Extensions.Bound;
using MG.Posh.Extensions.Shoulds;
using MG.Posh.Extensions.Writes;
using Microsoft.Management.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Reflection;
using Session = Microsoft.Management.Infrastructure.CimSession;

namespace MG.Core.PowerShell.Dhcp.Cmdlets
{
    public abstract class BaseDhcpCmdlet : PSCmdlet
    {
        private const string CMDLET_OUTPUT = "cmdletOutput";
        protected const string NAMESPACE = "ROOT\\Microsoft\\Windows\\DHCP";
        protected List<Session> _backingSessions { get; } = new List<Session>(1);

        #region PROPERTIES
        protected abstract string ClassName { get; set; }
        protected abstract bool IsSetting { get; }
        protected abstract string MethodName { get; set; }

        private CimMethodParametersCollection CimParameters { get; set; }

        #endregion

        #region PARAMETERS
        [Parameter(Mandatory = false)]
        public virtual Session[] CimSession
        {
            get => _backingSessions.ToArray();
            set => _backingSessions.AddRange(value);
        }

        #endregion

        #region CMDLET PROCESSING
        protected override void BeginProcessing()
        {
            if (!this.ContainsParameter(x => x.CimSession))
            {
                _backingSessions.Add(Session.Create(null));
            }
            _backingSessions.ForEach((x) =>
            {
                if (!x.TestConnection())
                {
                    var exc = new InvalidOperationException(string.Format("Cannot connect to {0}", x.ComputerName));
                    base.WriteError(new ErrorRecord(exc, exc.GetType().FullName, ErrorCategory.ConnectionError, x));
                }
            });
            this.CimParameters = new CimMethodParametersCollection();
        }

        protected override void ProcessRecord()
        {

        }

        protected override void EndProcessing()
        {

        }

        #endregion

        #region CMDLET HELPER METHODS

        protected string GetListOfComputerNames()
        {
            string servers = string.Join(", ", _backingSessions.Select(x => x.ComputerName));
            if (string.IsNullOrWhiteSpace(servers))
                servers = Environment.MachineName;

            return servers;
        }

        protected bool DefaultShouldProcess()
        {
            string servers = this.GetListOfComputerNames();
            return this.ShouldProcessFormat(this.MethodName, "Servers: {0}", servers);
        }

        #region WRITE ERROR
        protected void WriteError(uint errorCode) => this.WriteError("Returned an error code of {0}", errorCode);
        protected void WriteError(string messageFormat, params object[] messageArgs)
        {
            this.WriteError(ErrorCategory.InvalidResult, messageFormat, messageArgs);
        }
        protected void WriteError(ErrorCategory category, string messageFormat, params object[] messageArgs)
        {
            var ex = new CimException(string.Format(messageFormat, messageArgs));
            this.WriteError(ex, category);
        }
        protected void WriteError(Exception exception, ErrorCategory category) => this.WriteError(exception, category, null);
        protected void WriteError(Exception exception, ErrorCategory category, object target)
        {
            var errRec = new ErrorRecord(exception, exception.GetType().FullName, category, target);
            base.WriteError(errRec);
        }

        #endregion

        #endregion

        #region CIM EXECUTION METHODS
        private void CheckReturnCode(CimMethodResult result)
        {
            if (this.IsSetting && (uint)result.ReturnValue.Value == 0 && result.ReturnValue.Flags.HasFlag(CimFlags.NotModified))
                base.WriteWarning("Please restart the DHCP server service on garvmedia for the new setting to take effect.");
        }
        private static IEnumerable<CimInstance> FromResult(object cmdletOutput)
        {
            if (cmdletOutput is IEnumerable enumerable)
            {
                foreach (CimInstance feInst in enumerable)
                {
                    yield return feInst;
                }
            }
            else if (cmdletOutput is CimInstance single)
                yield return single;
        }
        protected IEnumerable<CimMethodResult> ExecuteStaticMethod()
        {

            foreach (Session cimSes in _backingSessions)
            {
                yield return cimSes.InvokeMethod(NAMESPACE, this.ClassName, this.MethodName, this.CimParameters);
            }
        }
        internal static IEnumerable<CimMethodResult> ExecuteStaticMethod(IEnumerable<Session> sessions, string className, string methodName, CimMethodParametersCollection parameters)
        {
            foreach (Session cimSes in sessions)
            {
                yield return cimSes.InvokeMethod(NAMESPACE, className, methodName, parameters);
            }
        }
        private static IEnumerable<CimInstance> ParseResults(CimMethodResult result, string outputParam = CMDLET_OUTPUT)
        {
            if (result == null || result.OutParameters == null || result.OutParameters.Count <= 0)
                return null;

            return FromResult(result.OutParameters[outputParam]?.Value);
        }
        protected void WriteResults<T>(IEnumerable<CimMethodResult> results, bool passThru = true, string outputParam = CMDLET_OUTPUT)
        {
            foreach (CimMethodResult result in results)
            {
                this.CheckReturnCode(result);

                IEnumerable<CimInstance> instances = ParseResults(result, outputParam);
                List<T> list = CimConverter.Deserialize<T>(instances);
                if (passThru)
                    base.WriteObject(list, true);
            }
        }

        #endregion

        #region PARAMETER METHODS
        protected void AddParameter<T>(T cmdlet, Expression<Func<T, object>> parameter, object value, CimFlags flags = CimFlags.In)
            where T: BaseDhcpCmdlet
        {
            string name = GetParameterName(cmdlet, parameter);
            CimMethodParameter cimParam = CimMethodParameter.Create(name, value, flags);
            this.CimParameters.Add(cimParam);
        }
        protected void AddParameter<T>(T cmdlet, Expression<Func<T, object>> parameter, object value, CimType type, CimFlags flags = CimFlags.In)
            where T : BaseDhcpCmdlet
        {
            string name = GetParameterName(cmdlet, parameter);
            CimMethodParameter cimParam = CimMethodParameter.Create(name, value, type, flags);
            this.CimParameters.Add(cimParam);
        }
        protected void AddParameters<T>(T cmdlet, params Expression<Func<T, object>>[] expressions)
            where T : BaseDhcpCmdlet
        {
            if (expressions == null || expressions.Length <= 0)
                return;

            foreach (Expression<Func<T, object>> exp in expressions)
            {
                string name = GetParameterName(cmdlet, exp);
                if (this.ContainsAnyParameterNames(name))
                {
                    CimMethodParameter parameter = NewParameter(cmdlet, name, exp);
                    this.CimParameters.Add(parameter);
                }
            }
        }
        internal static string GetParameterName<T>(T cmdlet, Expression<Func<T, object>> expression)
            where T : BaseDhcpCmdlet
        {
            string name = null;
            if (expression.Body is MemberExpression memEx)
                name = memEx.Member.Name;

            else if (expression.Body is UnaryExpression unEx && unEx.Operand is MemberExpression unExMem)
                name = unExMem.Member.Name;

            return name;
        }
        internal static CimMethodParameter NewParameter<T>(T cmdlet, string name, Expression<Func<T, object>> expression)
            where T : BaseDhcpCmdlet
        {
            var func = expression.Compile();
            object value = func(cmdlet);
            if (value is SwitchParameter swp)
                value = swp.ToBool();

            CimMethodParameter parameter = CimMethodParameter.Create(name, value, CimFlags.In);
            return parameter;
        }

        #endregion
    }
}