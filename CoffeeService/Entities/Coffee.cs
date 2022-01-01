using Framework.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeService.Entities
{
    [Table("Coffees",Schema ="Pdb")]
    public class Coffee:EntityBase
    {
        public Guid CoffeeId { get; set; }
        
        private Guid _coffeeTypeId;
        [Required]
        public Guid CoffeeTypeId
        {
            get => _coffeeTypeId;
            set
            {
                if (_coffeeTypeId == value) return;
                _coffeeTypeId = value;
                OnPropertyChanged();
            }
        }

        private DateTime   _productionDate;
        [Required]
        public DateTime ProductionDate
        {
            get => _productionDate;
            set
            {
                if (_productionDate == value) return;
                _productionDate = value;
                OnPropertyChanged();
            }
        }
        private int _quantity;
        [Required]
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity == value) return;
                _quantity = value;
                OnPropertyChanged();
            }
        }
        [ForeignKey("CoffeeTypeId")]
        public CoffeeType CoffeeType { get; set; }

        [InverseProperty("Coffee")]
        public List<CoffeeStore> CoffeeStores { get; set; }
    }
}
