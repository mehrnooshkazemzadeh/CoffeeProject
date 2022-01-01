using Framework.Core.Logic;
using StoreAPI.Entities;
using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Logic
{
    public interface IStoreScheduleLogic:ILogic<StoreScheduleModel>
    {
        List<StoreScheduleModel> GetStoreSchedules(Guid storeId);
    }
}
