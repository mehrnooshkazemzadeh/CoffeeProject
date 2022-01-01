using CoffeeService.Entities;
using CoffeeService.Models;
using Framework.Core;
using Framework.Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeService.Logic
{
    public interface ICoffeeLogic : ILogic<CoffeeModel>
    {
        CoffeeModel GetCoffeeByProductionDate(DateTime date, Guid coffeetypeId);
        List<CoffeeModel> GetCoffeeEntities();
    }
}
