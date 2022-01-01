using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Framework.Core.Domain
{
    public class EntityBase
    {
        public PropertyInfo GetKeyProperty()
        {
            var currentType = GetType();
            var isProxy = CheckIsProxy(currentType);
            var properties = currentType.GetProperties();
            var className = isProxy ? currentType.BaseType.Name : currentType.Name;
            var member = properties.FirstOrDefault(x => (x.GetCustomAttribute(typeof(KeyAttribute), true) != null) || x.Name ==
                                                        $"{className}Id");
            if (member == null)
            {

                throw new Exception("Key property not found.");
            }
            return member;
        }


        [NotMapped]
        public bool DisableTracking { get; set; }
        public bool CheckIsProxy(Type entityType)
        {
            return (entityType.BaseType != null && entityType.Namespace == "System.Data.Entity.DynamicProxies");
        }


        [NotMapped]
        public List<string> ModifiedProperties { get; set; }
        [NotMapped]
        public List<string> ModifiedNavigateProperties { get; set; }

        public object Key => GetKeyProperty().GetValue(this);


        protected EntityBase()
        {
            ModifiedProperties = new List<string>();
            ModifiedNavigateProperties = new List<string>();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (DisableTracking) return;
            ModifiedProperties.Add(propertyName);
        }
        protected void OnNavigatePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (DisableTracking) return;
            ModifiedNavigateProperties.Add(propertyName);
        }
    }
}
