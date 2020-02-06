using System;
using System.Collections.Generic;
using System.Web;
using System.Reflection;
using System.ComponentModel;

/// <summary>
/// Summary description for Enumerators
/// </summary>
namespace Utilities
{
    public static class Enumerators
    {
        public static T Parse<T>(object value)
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                return (T)Enum.Parse(typeof(T), value.ToString());
            }

            return default(T);
        }

        public static string GetDescription(object value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo is FieldInfo)
            {
                DescriptionAttribute[] descriptions = ((DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false));

                if (descriptions.Length > 0)
                {
                    return descriptions[0].Description;
                }
            }

            return null;
        }
    }
}