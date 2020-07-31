using MG.Core.PowerShell.Dhcp.Converters;
using MG.Core.PowerShell.Dhcp.Models;
using Microsoft.Management.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MG.Core.PowerShell.Dhcp
{
    internal static class CimConverter
    {
        public static List<T> Deserialize<T>(IEnumerable<CimInstance> instances)
        {
            var list = new List<T>();
            if (instances == null)
                return list;

            foreach (CimInstance instance in instances)
            {
                list.Add(Deserialize<T>(instance));
            }
            return list;
        }
        public static T Deserialize<T>(CimInstance instance)
        {
            if (instance == null)
                return default;

            T oT = Activator.CreateInstance<T>();
            PopulateInstance(oT, instance);
            return oT;
        }

        #region PRIVATE
        private static void PopulateInstance<T>(T obj, CimInstance instance)
        {
            IEnumerable<string> propKeys = instance.CimInstanceProperties.Select(x => x.Name);
            foreach (MemberInfo mi in obj.GetType().GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.GetCustomAttribute<CimPropertyAttribute>() != null))
            {

                if (mi is PropertyInfo pi)
                {
                    PopulateFromProperty(obj, pi, instance, propKeys, pi.PropertyType);
                }
                else if (mi is FieldInfo fi)
                {
                    PopulateFromField(obj, fi, instance, propKeys, fi.FieldType);
                }

                else
                    continue;
            }
        }

        private static object ConvertObject(object rawValue, CimPropertyAttribute attribute)
        {
            IConverter inst = (IConverter)Activator.CreateInstance(attribute.Converter);
            return inst.ConvertValue(rawValue);
        }

        private static void PopulateFromProperty<T>(T obj, PropertyInfo pi, CimInstance instance, IEnumerable<string> propKeys, Type type)
        {
            CimPropertyAttribute att = pi.GetCustomAttribute<CimPropertyAttribute>();
            string name = att.Name;
            if (string.IsNullOrEmpty(name))
                name = pi.Name;

            if (propKeys.Contains(name))
            {
                object value = instance.CimInstanceProperties[name].Value;
                if (att.Converter != null)
                    value = ConvertObject(value, att);

                pi.SetValue(obj, value);
            }
        }

        private static void PopulateFromField<T>(T obj, FieldInfo fi, CimInstance instance, IEnumerable<string> propKeys, Type type)
        {
            CimPropertyAttribute att = fi.GetCustomAttribute<CimPropertyAttribute>();
            string name = att.Name;
            if (string.IsNullOrEmpty(name))
                name = fi.Name;

            if (propKeys.Contains(name))
            {
                object value = instance.CimInstanceProperties[name].Value;
                if (att.Converter != null)
                    value = ConvertObject(value, att);

                fi.SetValue(obj, value);
            }
        }

        #endregion
    }
}
