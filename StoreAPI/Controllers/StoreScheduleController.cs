using Framework.Core.Logic;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Logic;
using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreScheduleController : ControllerBase
    {
        private readonly IStoreScheduleLogic logic;

        public StoreScheduleController(IStoreScheduleLogic logic)
        {
            this.logic = logic;
        }
        // GET: api/<StoreScheduleController>
        [HttpGet]
        public List<StoreScheduleModel> GetAll()
        {
            var result = logic.GetAll();
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return new List<StoreScheduleModel>();
        }


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [Route("GetById/{id}")]
        public StoreScheduleModel GetById(Guid id)
        {
            var result = logic.GetRow(id);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return null;
        }

        [Route("Store/{id}")]
        [HttpGet("{id}")]
        public List<StoreScheduleModel> GetByStoreId(Guid id)
        {
            return logic.GetStoreSchedules(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public bool Insert([FromBody] StoreScheduleModel storeSchedule)
        {
            var result = logic.AddNew(storeSchedule);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return true;
            return false;
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        public bool Update( [FromBody] StoreScheduleModel storeSchedule)
        {
            var result = logic.Update(storeSchedule);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return false;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var result = logic.Delete(id);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return true;
            return false;
        }
    }
}
