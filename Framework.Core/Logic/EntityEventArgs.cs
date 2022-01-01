using Framework.Core.Domain;

namespace Framework.Core.Logic
{
    public class EntityEventArgs<TEntity> where TEntity : EntityBase
    {
        public TEntity NewEntity { get; set; }
        public EntityAction EntityAction { get; set; }
        public TEntity OldEntity { get; set; }


    }
}
