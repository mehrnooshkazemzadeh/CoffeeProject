using Framework.Core.Domain;
using StoreAPI.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Entities
{
    [Table("CoffeeStores",Schema ="Str")]
    public class CoffeeStore:EntityBase
    {
        public Guid CoffeeStoreId { get; set; }

        private Guid _cofeeId;
        [Required]
        public Guid CoffeeId
        {
            get => _cofeeId;
            set
            {
                if (_cofeeId == value) return;
                _cofeeId = value;
                OnPropertyChanged();
            }
        }
        private Guid _storeId;
        [Required]
        public Guid StoreId
        {
            get => _storeId;
            set
            {
                if (_storeId == value) return;
                _storeId = value;
                OnPropertyChanged();
            }
        }
        private PostStatusEnum _poststatus;
        [Required]
        public PostStatusEnum Poststatus
        {
            get => _poststatus;
            set
            {
                if (_poststatus == value) return;
                _poststatus = value;
                OnPropertyChanged();
            }
        }

        private DateTime  _postDate;
        [Required]
        public DateTime PostDate
        {
            get => _postDate;
            set
            {
                if (_postDate == value) return;
                _postDate = value;
                OnPropertyChanged();
            }
        }
        private DateTime? _recievedDate;
        public DateTime? RecievedDate
        {
            get => _recievedDate;
            set
            {
                if (_recievedDate == value) return;
                _recievedDate = value;
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
    }
}
