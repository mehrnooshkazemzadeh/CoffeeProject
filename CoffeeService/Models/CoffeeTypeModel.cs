using CoffeeService.Entities;
using Framework.Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeService.Models
{
    public class CoffeeTypeModel:ModelBase<CoffeeType, Guid>
    {
        public Guid CoffeeTypeId { get; set; }
        public string Title { get; set; }
        public int QuantityInPack { get; set; }
    }
}
