using Framework.Core.Logic;
using StoreAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Models
{
    public class StoreModel:ModelBase<Store,Guid>
    {
        public Guid StoreId { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
