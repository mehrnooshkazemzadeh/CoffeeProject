using Framework.Core.Logic;
using StoreAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Models
{
    public class CoffeeStoreModel:ModelBase<CoffeeStore,Guid>
    {
        public Guid CoffeeStoreId { get; set; }
        public Guid CoffeeId { get; set; }
        public int Quantity { get; set; }
    }
}
