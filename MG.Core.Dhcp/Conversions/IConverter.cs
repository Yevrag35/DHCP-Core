using System;
using System.Collections.Generic;
using System.Text;

namespace MG.Core.PowerShell.Dhcp.Converters
{
    public interface IConverter
    {
        object ConvertValue(object rawValue);
    }
}
