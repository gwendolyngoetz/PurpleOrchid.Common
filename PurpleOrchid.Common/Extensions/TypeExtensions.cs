using System;
using System.Collections.Generic;
using System.Reflection;
using PurpleOrchid.Common.Contracts;

namespace PurpleOrchid.Common.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetPublicProperties(this Type source)
        {
            Require.NotNull(nameof(source), source);
            return source.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
        }
    }
}
