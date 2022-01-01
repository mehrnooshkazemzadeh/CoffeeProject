using Coffee.APIProvider;
using Coffee.WebApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Coffee.WebApplication.Services
{
    public class StoreScheduleService:APIProvider<StoreScheduleModel>,IStoreScheduleService
    {
        public async Task<List<StoreScheduleModel>> GetByStoreId(Guid storeId)
        {
            var result =await httpService.GetAsync($"Store/{storeId}");
            if (result.IsSuccessStatusCode)
            {
                var contentResult = result.Content.ReadAsStringAsync().Result;
                var storeSchedules= JsonConvert.DeserializeObject<List<StoreScheduleModel>>(contentResult);
                return storeSchedules;
            }
            return null;
        }

    }
}
