using Mapster;
using Framework.Core.Domain;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Framework.Core.Logic
{

    public class ModelBase<TEntity, TKey>
        where TEntity : EntityBase
    {

        public TKey Key
        {
            get => (TKey)GetType().GetProperty(GetKeyPropertyName()).GetValue(this);
            set => GetType().GetProperty(GetKeyPropertyName()).SetValue(this, value);
        }

        public string GetKeyPropertyName()
        {
            var keyProperty = GetType().GetProperties().FirstOrDefault(x => x.GetCustomAttributes().Any(z => z is KeyAttribute));
            if (keyProperty != null) return keyProperty.Name;
            keyProperty = GetType().GetProperties().FirstOrDefault(x => x.Name == typeof(TEntity).Name + "Id");
            if (keyProperty != null) return keyProperty.Name;
            return null;
        }


        public TEntity GetDomain()
        {
            return this.Adapt<TEntity>();
        }

    }
}
