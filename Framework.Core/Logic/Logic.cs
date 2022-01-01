using System;
using System.Collections.Generic;
namespace Framework.Core.Logic
{
    public class Logic<TModel> : ILogic<TModel>
    {
        private readonly ILogic<TModel> _instance;
        public bool Cachable { get => _instance.Cachable; set => _instance.Cachable = value; }

        public Logic(IServiceProvider serviceProvider)
        {
            var model = Activator.CreateInstance<TModel>();

            var getDomain = typeof(TModel).GetMethod("GetDomain");
            var key = typeof(TModel).GetProperty("Key");
            var domain = getDomain.Invoke(model, null);

            var operationType = typeof(IBusinessOperations<,,>);
            var genericType = operationType.MakeGenericType(typeof(TModel), domain.GetType(), key.PropertyType);

            _instance = (ILogic<TModel>)serviceProvider.GetService(genericType);
        }

        public BusinessOperationResult<TModel> AddNew(TModel newItem)
        {
            return _instance.AddNew(newItem);
        }

        public BusinessOperationResult<long> Count()
        {
            return _instance.Count();
        }

        public BusinessOperationResult<bool> Delete(object key)
        {
            return _instance.Delete(key);
        }

        public BusinessOperationResult<bool> Delete(List<object> keys)
        {
            return _instance.Delete(keys);
        }

        public BusinessOperationResult<List<TModel>> GetAll()
        {
            return _instance.GetAll();
        }

        public BusinessOperationResult<List<TModel>> GetAll(int row, int max)
        {
            return _instance.GetAll(row, max);
        }

        public BusinessOperationResult<List<TModel>> GetAll(int row, int max, List<SortInformation> sortInformations)
        {
            return _instance.GetAll(row, max, sortInformations);
        }

        public BusinessOperationResult<List<TModel>> GetAll(int row, int max, ref long count)
        {
            return _instance.GetAll(row, max, ref count);
        }

        public BusinessOperationResult<List<TModel>> GetAll(Dictionary<string, object> filters)
        {
            return _instance.GetAll(filters);
        }

        public BusinessOperationResult<List<TModel>> GetAll(Dictionary<string, object> filters, int row, int max)
        {
            return _instance.GetAll(filters, row, max);
        }

        public BusinessOperationResult<TModel> GetRow(object key)
        {
            return _instance.GetRow(key);
        }

        public BusinessOperationResult<TModel> GetRow(object key, List<string> includes)
        {
            return _instance.GetRow(key, includes);
        }

        public BusinessOperationResult<bool> Update(TModel item)
        {
            return _instance.Update(item);
        }

        public BusinessOperationResult<List<TModel>> GetAll(Dictionary<string, object> filters, int row, int max, List<SortInformation> sortInformations)
        {
            return _instance.GetAll(filters, row, max, sortInformations);
        }

        public List<BusinessOperationResult<bool>> Update(List<TModel> items)
        {
            return _instance.Update(items);
        }

        public BusinessOperationResult<List<TModel>> GetAll(int row, int max, List<SortInformation> sortInformations, List<string> includes)
        {
            return _instance.GetAll(row, max, sortInformations, includes);
        }
    }
}
