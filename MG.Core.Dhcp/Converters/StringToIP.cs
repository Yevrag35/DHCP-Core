using System;
using System.Collections.Generic;
using System.Net;

namespace MG.Core.PowerShell.Dhcp.Converters
{
    public class StringToIP : ConverterClass<string, IPAddress>
    {
        public StringToIP() { }

        public override IPAddress ConvertValue(string rawValue)
        {
            return IPAddress.Parse(rawValue);
        }
    }
}
