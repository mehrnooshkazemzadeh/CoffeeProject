using Coffee.APIProvider;
using CoffeeFactory.WindowsService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeFactory.WindowsService.Services
{
    public interface ICoffeeService:IAPIProvider<CoffeeModel>
    {
        Task<CoffeeModel> GetTodayCoffeeByCoffeeType( Guid id);
    }
}
