using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeFactory.WindowsService.Models
{
    public class CoffeeModel
    {
        public Guid CoffeeId { get; set; }
        public Guid CoffeeTypeId { get; set; }
        public DateTime ProductionDate { get; set; }
        public int Quantity { get; set; }
    }
}
