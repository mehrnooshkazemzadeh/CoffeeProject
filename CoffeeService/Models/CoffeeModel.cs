using CoffeeService.Entities;
using Framework.Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeService.Models
{
    public class CoffeeModel:ModelBase<Coffee,Guid>
    {
        public Guid CoffeeId { get; set; }
        public Guid CoffeeTypeId { get; set; }
        public DateTime ProductionDate { get; set; }
        public int Quantity { get; set; }
        public CoffeeTypeModel CoffeeTypeModel { get; set; }
    }
}
