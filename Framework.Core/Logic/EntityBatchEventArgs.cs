using Framework.Core.Domain;
using System;
using System.Linq.Expressions;

namespace Framework.Core.Logic
{
    public class EntityBatchEventArgs<TEntity> where TEntity : EntityBase
    {
        public Expression<Func<TEntity, bool>> Predicate { get; set; }
        public EntityAction EntityAction { get; set; }

    }
}
