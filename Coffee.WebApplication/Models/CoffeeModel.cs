using Framework.Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee.WebApplication.Models
{
    public class CoffeeModel
    {
        public Guid CoffeeId { get; set; }
        public Guid CoffeeTypeId { get; set; }
        public DateTime ProductionDate { get; set; }
        public int Quantity { get; set; }
        public int PackQuantity { get => CoffeeTypeModel == null ? 0 : Quantity / CoffeeTypeModel.QuantityInPack; }
        public int SendedPackQuantity { get; set; }
        public CoffeeTypeModel CoffeeTypeModel { get; set; }

    }
}
