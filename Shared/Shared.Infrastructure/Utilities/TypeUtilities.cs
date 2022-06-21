using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModularArchitecture.Shared.Infrastructure.Utilities
{
    public static class TypeUtilities
    {
        public static List<string> GetNestedClassesStaticStringValues(this Type type)
        {
            List<string> values = new List<string>();
            foreach (var prop in type.GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                object propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                {
                    values.Add(propertyValue.ToString());
                }
            }

            return values;
        }
    }
}
