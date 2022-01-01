using System.Collections.Generic;
namespace Framework.Core.Logic
{
    public interface ILogic<TModel>
    {
        bool Cachable { get; set; }

        BusinessOperationResult<List<TModel>> GetAll();
        BusinessOperationResult<List<TModel>> GetAll(int row, int max);
        BusinessOperationResult<List<TModel>> GetAll(int row, int max, List<SortInformation> sortInformations);
        BusinessOperationResult<List<TModel>> GetAll(int row, int max, ref long count);
        BusinessOperationResult<List<TModel>> GetAll(Dictionary<string, object> filters);
        BusinessOperationResult<List<TModel>> GetAll(Dictionary<string, object> filters, int row, int max);
        BusinessOperationResult<List<TModel>> GetAll(Dictionary<string, object> filters, int row, int max, List<SortInformation> sortInformations);
        BusinessOperationResult<List<TModel>> GetAll(int row, int max, List<SortInformation> sortInformations,List<string> includes);

        BusinessOperationResult<bool> Delete(object key);
        BusinessOperationResult<bool> Delete(List<object> keys);
        BusinessOperationResult<TModel> GetRow(object key);
        BusinessOperationResult<TModel> GetRow(object key, List<string> includes);
        BusinessOperationResult<TModel> AddNew(TModel newItem);

        BusinessOperationResult<bool> Update(TModel item);
        List<BusinessOperationResult<bool>> Update(List<TModel> items);
        BusinessOperationResult<long> Count();
    }
}
