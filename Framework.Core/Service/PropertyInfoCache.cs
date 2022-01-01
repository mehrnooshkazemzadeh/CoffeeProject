using System.Collections.Generic;
using System.Reflection;

namespace Framework.Core.Service
{

    /// <summary>
    /// Keeps a mapping between a string and a PropertyInfo instance.
    /// Simply wraps an IDictionary and exposes the relevant operations.
    /// Putting all this in a separate class makes the calling code more
    /// readable.
    /// </summary>
    internal class PropertyInfoCache
    {
        private readonly IDictionary<string, PropertyInfo> _propertyInfoCache;

        public PropertyInfoCache()
        {
            _propertyInfoCache = new Dictionary<string, PropertyInfo>();
        }

        public bool ContainsKey(string key)
        {
            return _propertyInfoCache.ContainsKey(key);
        }

        public void Add(string key, PropertyInfo value)
        {
            _propertyInfoCache.Add(key, value);
        }

        public PropertyInfo this[string key]
        {
            get => _propertyInfoCache[key];
            set => _propertyInfoCache[key] = value;
        }
    }
}
