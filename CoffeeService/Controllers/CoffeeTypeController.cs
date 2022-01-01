using CoffeeService.Entities;
using CoffeeService.Logic;
using CoffeeService.Models;
using Framework.Core.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoffeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeTypeController : ControllerBase
    {
        private readonly ILogger<CoffeeTypeController> logger;

        private ILogic<CoffeeTypeModel> coffeeTypeLogic;

        public CoffeeTypeController(ILogger<CoffeeTypeController> logger, ILogic<CoffeeTypeModel> coffeeTypeLogic)
        {
            this.logger = logger;
            this.coffeeTypeLogic = coffeeTypeLogic;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<CoffeeTypeModel> Get()
        {
            var result = coffeeTypeLogic.GetAll();
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return new List<CoffeeTypeModel>();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public CoffeeTypeModel GetById(Guid id)
        {
            var result = coffeeTypeLogic.GetRow(id);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return null;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public bool Insert([FromBody] CoffeeTypeModel coffeeType)
        {
            var result = coffeeTypeLogic.AddNew(coffeeType);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return true;
            return false;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public bool Update([FromBody] CoffeeTypeModel coffeeType)
        {
            var result = coffeeTypeLogic.Update(coffeeType);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return true;
            return false;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var result = coffeeTypeLogic.Delete(id);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return true;
            return false;
        }
    }
}
