using Framework.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeService.Entities
{
    [Table("CoffeeTypes",Schema ="Pdb")]
    public class CoffeeType:EntityBase
    {
        public Guid CoffeeTypeId { get; set; }

        private string _title;
        [StringLength(20)]
        [Required]
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        private int _quantityInPack;
        [Required]
        public int QuantityInPack
        {
            get => _quantityInPack;
            set
            {
                if (_quantityInPack == value) return;
                _quantityInPack = value;
                OnPropertyChanged();
            }
        }
    }
}
