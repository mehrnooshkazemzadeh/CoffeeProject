using Framework.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Entities
{
    [Table("StoreSchedules",Schema ="Str")]
    public class StoreSchedule:EntityBase
    {
        public Guid StoreScheduleId { get; set; }

        private Guid _storeId;
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
        private string  _day;
        [StringLength(10)]
        [Required]
        public string Day
        {
            get => _day;
            set
            {
                if (_day == value) return;
                _day = value;
                OnPropertyChanged();
            }
        }

        private DateTime _startTime;
        [Required]
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if (_startTime == value) return;
                _startTime = value;
                OnPropertyChanged();
            }
        }

        private DateTime _endTime;
        [Required]
        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                if (_endTime == value) return;
                _endTime = value;
                OnPropertyChanged();
            }
        }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
    }
}
