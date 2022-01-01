using Coffee.APIProvider;
using Coffee.WebApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Coffee.WebApplication.Services
{
    public class CoffeeService:APIProvider<CoffeeModel>,ICoffeeService
    {
        public async Task<List<CoffeeModel>> GetCoffeeEntities()
        {
            var result = await httpService.GetAsync("GetCoffeeEntities");
            if (result.IsSuccessStatusCode)
            {
                var storeList = result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<CoffeeModel>>(storeList);
            }
            return new List<CoffeeModel>();
        }

    }
}

