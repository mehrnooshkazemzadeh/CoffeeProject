using Framework.Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee.WebApplication.Models
{
    public class CoffeeStoreModel
    {
        public Guid CoffeeStoreId { get; set; }
        public Guid CoffeeId { get; set; }
        public Guid StoreId { get; set; }
        public int Poststatus { get; set; }
        public DateTime PostDate { get; set; }
        DateTime? _recievedDate = DateTime.Now.Date;
        public DateTime? RecievedDate { get => _recievedDate; set { _recievedDate = value; } }
        public int Quantity { get; set; }
        public string PostStatusName { get; set; }
        public bool Checked { get; set; }
        public string CoffeeTypeName { get; set; }
   
    }
}
