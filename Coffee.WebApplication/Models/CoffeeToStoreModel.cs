using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee.WebApplication.Models
{
    public class CoffeeToStoreModel
    {
        public List<CoffeeModel> CoffeeModels { get; set; }
        public List<StoreModel> Stores { get; set; }
        [Required(ErrorMessage = "Store is required")]
        public Guid StoreId { get; set; }
        DateTime _postDate = DateTime.Now.Date;
        [Required(ErrorMessage = "PostDate is required")]
        public DateTime PostDate
        {
            get => _postDate;
            set
            {
                _postDate = value;
            }
        }
    }
}
