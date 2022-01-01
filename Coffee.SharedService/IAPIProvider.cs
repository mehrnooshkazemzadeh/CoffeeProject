using Framework.Core.Logic;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.APIProvider
{
    public interface IAPIProvider<TModel>
    {
        void Initialize(string apiAddress);
        Task<List<TModel>> GetAll();
        Task<TModel> GetById(string actionName,Guid id);
        Task<TModel> Insert(TModel model);
        bool Update(TModel model);
        bool Delete(Guid id);
    }
}
