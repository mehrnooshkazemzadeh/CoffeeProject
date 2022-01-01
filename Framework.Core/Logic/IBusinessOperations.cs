using Framework.Core.Domain;
namespace Framework.Core.Logic
{
    public interface IBusinessOperations<TModel, TEntity, TKey> : ILogic<TModel>
            where TModel : ModelBase<TEntity, TKey>
        where TEntity : EntityBase

    {


    }
}
