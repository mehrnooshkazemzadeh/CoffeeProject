using CoffeeService.Entities;
using CoffeeService.Enums;
using Framework.Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeService.Models
{
    public class CoffeeStoreModel:ModelBase<CoffeeStore,Guid>
    {
        public Guid CoffeeStoreId { get; set; }
        public Guid CoffeeId { get; set; }
        public Guid StoreId { get; set; }
        public PostStatusEnum Poststatus { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime? RecievedDate { get; set; }
        public int Quantity { get; set; }
        public string PostStatusName { get => Poststatus.ToString(); }
        string _coffyTypeName;
        public string CoffeeTypeName {
            //get => Coffee != null && Coffee.CoffeeType != null ? Coffee.CoffeeType.Title : null; 
            get => ""; set { }
        }
        public Guid CoffeeTypeId{ get; set; }
        public Coffee Coffee { get; set; }
    }
}
