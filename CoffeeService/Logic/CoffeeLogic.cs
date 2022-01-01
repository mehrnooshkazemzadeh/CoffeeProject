using CoffeeService.Entities;
using CoffeeService.Models;
using Framework.Core.Logic;
using Framework.Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeService.Logic
{
    public class CoffeeLogic : BusinessOperations<CoffeeModel, Coffee, Guid>, ICoffeeLogic
    {
        public CoffeeLogic(IPersistenceService<Coffee> service) : base(service)
        {
        }

        public CoffeeModel GetCoffeeByProductionDate(DateTime date, Guid coffeeTypeId)
        {
            var result = GetFirst<CoffeeModel>(x => x.ProductionDate.Date == date.Date && x.CoffeeTypeId == coffeeTypeId);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return null;
        }

        public List<CoffeeModel> GetCoffeeEntities()
        {
            var coffees = Service.Entities.Include(x=>x.CoffeeStores).Include(x=>x.CoffeeType).Where(x => x.Quantity - x.CoffeeStores.Sum(y => y.Quantity) > 0).ToList();
            var coffeeModels = new List<CoffeeModel>();
            coffees.ForEach(x =>
               coffeeModels.Add(new CoffeeModel
               {
                   Quantity = x.Quantity - x.CoffeeStores.Sum(y => y.Quantity),
                   CoffeeId = x.CoffeeId,
                   CoffeeTypeId = x.CoffeeTypeId,
                   ProductionDate = x.ProductionDate,
                   CoffeeTypeModel = new CoffeeTypeModel
                   {
                       CoffeeTypeId = x.CoffeeType.CoffeeTypeId,
                       QuantityInPack =x.CoffeeType.QuantityInPack,
                       Title = x.CoffeeType.Title
                   }
               })); ;
            return coffeeModels;
        }
    }
}
