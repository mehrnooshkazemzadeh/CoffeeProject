using Framework.Core.Logic;
using Microsoft.AspNetCore.Mvc;
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
    public class StoreController : ControllerBase
    {
        private readonly ILogic<StoreModel> logic;

        public StoreController(ILogic<StoreModel> logic)
        {
            this.logic = logic;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public List<StoreModel> Get()
        {
            var result =logic.GetAll();
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return new List<StoreModel>();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public StoreModel GetById(Guid id)
        {
            var result = logic.GetRow(id);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return null;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public StoreModel Insert([FromBody] StoreModel store)
        {
            var result = logic.AddNew(store);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return null;
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        public bool Update([FromBody] StoreModel store)
        {
            var result = logic.Update(store);
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
