using Coffee.APIProvider;
using CoffeeFactory.WindowsService.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeFactory.WindowsService.Services
{
    public class CoffeeService : APIProvider<CoffeeModel>, ICoffeeService
    {
        public async Task<CoffeeModel> GetTodayCoffeeByCoffeeType(Guid id)
        {
            var result = await httpService.GetAsync($"GetTodayCoffeeByCoffeeType/{id}");
            if (result.IsSuccessStatusCode)
            {
                var contentResult = result.Content.ReadAsStringAsync().Result;
                var storeSchedules = JsonConvert.DeserializeObject<CoffeeModel>(contentResult);
                return storeSchedules;
            }
            return null;
        }
    }
}
