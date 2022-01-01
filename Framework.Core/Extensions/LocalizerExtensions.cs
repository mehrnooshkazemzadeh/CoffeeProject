using Microsoft.Extensions.Localization;
using System;
using System.Reflection;

namespace Framework.Core.Extensions
{
    public static class LocalizerExtensions
    {
        public static IStringLocalizer GetLocalizer(this Type type,IServiceProvider serviceProvider)
        {
            var method = typeof(LocalizerExtensions).GetMethod(nameof(GetGenericLocalizer), BindingFlags.Static | BindingFlags.NonPublic );
            if (type.ContainsGenericParameters) return null;
            return (IStringLocalizer)method.MakeGenericMethod(type).Invoke(null, new[] { serviceProvider });
        }
        public static IStringLocalizer GetLocalizer(this TypeInfo type, IServiceProvider serviceProvider)
        {
            var method = typeof(LocalizerExtensions).GetMethod(nameof(GetGenericLocalizer), BindingFlags.Static | BindingFlags.NonPublic);
            if (type.ContainsGenericParameters) return null;
            return (IStringLocalizer)method.MakeGenericMethod(type).Invoke(null, new[] { serviceProvider });
        }
        private static IStringLocalizer<T> GetGenericLocalizer<T>(IServiceProvider serviceProvider)
        {
            return (IStringLocalizer<T>)serviceProvider.GetService(typeof(IStringLocalizer<T>));
        }
    }
}
