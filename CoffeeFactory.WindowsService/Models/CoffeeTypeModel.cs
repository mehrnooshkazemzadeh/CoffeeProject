using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeFactory.WindowsService.Models
{
    public class CoffeeTypeModel
    {
        public Guid CoffeeTypeId { get; set; }
        public string Title { get; set; }
        public int QuantityInPack { get; set; }
    }
}
