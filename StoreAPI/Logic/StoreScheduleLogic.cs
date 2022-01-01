
using Framework.Core.Logic;
using Framework.Core.Service;
using Microsoft.Extensions.Logging;
using StoreAPI.Entities;
using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Logic
{
    public class StoreScheduleLogic : BusinessOperations<StoreScheduleModel, StoreSchedule, Guid> ,IStoreScheduleLogic
    {
        public StoreScheduleLogic(IPersistenceService<StoreSchedule> service) : base(service)
        {

        }
        public List<StoreScheduleModel> GetStoreSchedules(Guid storeId)
        {
            var result =GetData<StoreScheduleModel>(x => x.StoreId == storeId);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return new List<StoreScheduleModel>();
        }
    }
}
