using Framework.Core.Logic;
using StoreAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Models
{
    public class StoreScheduleModel: ModelBase<StoreSchedule, Guid>
    {
        public Guid StoreScheduleId { get; set; }
        [Required(ErrorMessage ="Store is Required")]
        public Guid StoreId
        {
            get;set;
        }
        [Required(ErrorMessage ="Day is Required")]
        public string Day
        {
            get;set;
        }

        [Required(ErrorMessage ="StartTime is Required")]
        public DateTime StartTime
        {
            get;set;
        }

        private DateTime _endTime;
        [Required(ErrorMessage ="EndTime is Required")]
        public DateTime EndTime
        {
            get;set;
        }
    }
}
