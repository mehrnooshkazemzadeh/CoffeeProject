using Coffee.APIProvider;
using Coffee.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Coffee.WebApplication.Services
{
    public interface IStoreScheduleService:IAPIProvider<StoreScheduleModel>
    {
        Task<List<StoreScheduleModel>> GetByStoreId(Guid storeId);


    }
}
