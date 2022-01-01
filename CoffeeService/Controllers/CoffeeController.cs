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
    public class CoffeeController : ControllerBase
    {
        private readonly ILogger<CoffeeController> logger;

        private ICoffeeLogic coffeeLogic;

        public CoffeeController(ILogger<CoffeeController> logger, ICoffeeLogic coffeeLogic)
        {
            this.logger = logger;
            this.coffeeLogic = coffeeLogic;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<CoffeeModel> Get()
        {
            var result = coffeeLogic.GetAll();
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return new List<CoffeeModel>();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [Route("GetById/{id}")]
        public CoffeeModel GetById(Guid id)
        {
            var result = coffeeLogic.GetRow(id);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return null;
        }

        [HttpGet("{id}")]
        [Route("GetTodayCoffeeByCoffeeType/{id}")]
        public CoffeeModel GetTodayCoffeeByCoffeeType(Guid id)
        {
            return coffeeLogic.GetCoffeeByProductionDate(DateTime.Now, id);
        }

        [HttpGet()]
        [Route("GetCoffeeEntities")]
        public List<CoffeeModel> GetCoffeeEntities()
        {
            return coffeeLogic.GetCoffeeEntities();
        }


        // POST api/<ValuesController>
        [HttpPost]
        public CoffeeModel Insert([FromBody] CoffeeModel coffee)
        {
            var result = coffeeLogic.AddNew(coffee);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return null;
        }

        // PUT api/<ValuesController>/5
        [HttpPut()]
        public bool Update([FromBody] CoffeeModel coffee)
        {
            var result = coffeeLogic.Update(coffee);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return true;
            return false;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var result = coffeeLogic.Delete(id);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return true;
            return false;
        }
    }
}
