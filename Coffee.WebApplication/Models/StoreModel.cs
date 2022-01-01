using Framework.Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee.WebApplication.Models
{
    public class StoreModel
    {
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string FullName { get => $"{Name}({Address})"; }
    }
}
