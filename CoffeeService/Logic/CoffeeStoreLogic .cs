using CoffeeService.Entities;
using CoffeeService.Models;
using Framework.Core.Logic;
using Framework.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeService.Logic
{
    public class CoffeeStoreLogic : BusinessOperations<CoffeeStoreModel, CoffeeStore, Guid>, ICoffeeStoreLogic
    {
        public CoffeeStoreLogic(IPersistenceService<CoffeeStore> service) : base(service)
        {
        }

        public List<CoffeeStoreModel> GetInventory(Guid? storeId, Guid? coffeeTypeId)
        {
           var result = GetData<CoffeeStoreModel>(x =>
            (!storeId.HasValue || x.StoreId == storeId)
            && (!coffeeTypeId.HasValue || x.Coffee.CoffeeTypeId == coffeeTypeId) && x.Poststatus == Enums.PostStatusEnum.Recieved);
            if (result.ResultStatus == OperationResultStatus.Successful)
            {
                return result.ResultEntity.GroupBy(x => new { x.StoreId, x.Coffee.CoffeeType }).Select(x =>
                  new CoffeeStoreModel
                  {
                      StoreId = x.Key.StoreId,
                      CoffeeTypeName = x.Key.CoffeeType.Title,
                      CoffeeTypeId = x.Key.CoffeeType.CoffeeTypeId,
                      Quantity = x.Sum(y => y.Quantity)
                  }
                ).ToList();
            }
            return new List<CoffeeStoreModel>();
            
        }

    }
}
