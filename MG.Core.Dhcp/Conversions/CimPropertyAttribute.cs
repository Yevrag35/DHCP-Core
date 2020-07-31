using MG.Core.PowerShell.Dhcp.Converters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MG.Core.PowerShell.Dhcp
{
    internal class CimPropertyAttribute : Attribute
    {
        public Type Converter { get; }
        public string Name { get; }

        public CimPropertyAttribute() { }
        public CimPropertyAttribute(string name) => this.Name = name;
        public CimPropertyAttribute(Type converterType) => this.Converter = converterType;
        public CimPropertyAttribute(string name, Type converterType) : this(converterType)
        {
            this.Name = name;
        }
    }

    public abstract class ConverterClass<T1, T2> : IConverter
    {
        public abstract T2 ConvertValue(T1 rawValue);
        object IConverter.ConvertValue(object rawValue) => this.ConvertValue((T1)rawValue);
    }
}
