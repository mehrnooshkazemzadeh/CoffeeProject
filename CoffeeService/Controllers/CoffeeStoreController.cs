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
    public class CoffeeStoreController : ControllerBase
    {
        private readonly ILogger<CoffeeStoreController> logger;

        private ILogic<CoffeeStoreModel> coffeeStoreLogic;

        public CoffeeStoreController(ILogger<CoffeeStoreController> logger, ILogic<CoffeeStoreModel> coffeeStoreLogic)
        {
            this.logger = logger;
            this.coffeeStoreLogic = coffeeStoreLogic;
        }
        // GET: api/<CoffeeStoreController>
        [HttpGet]
        public List<CoffeeStoreModel> Get()
        {
            var incluedList = new List<string> { "Coffee.CoffeeType" };
            var result = coffeeStoreLogic.GetAll(0,1000 , null ,incluedList);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return new List<CoffeeStoreModel>();
        }

        // GET api/<CoffeeStoreController>/5
        [HttpGet]
        [Route("GetCoffeeInventory/{id}/{storeId}")]
        public List<CoffeeModel> Get(Guid id , Guid storeId)
        {
            return new List<CoffeeModel>();
            //coffeeStoreLogic.get
        }

        // POST api/<CoffeeStoreController>
        [HttpPost]
        public CoffeeStoreModel Insert([FromBody] CoffeeStoreModel coffeeStoreModel)
        {
            var result = coffeeStoreLogic.AddNew(coffeeStoreModel);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return result.ResultEntity;
            return null;
        }

        // PUT api/<CoffeeStoreController>/5
        [HttpPut()]
        public bool Update([FromBody] CoffeeStoreModel coffeeStoreModel)
        {
            var result = coffeeStoreLogic.Update(coffeeStoreModel);
            if (result.ResultStatus == OperationResultStatus.Successful)
                return true;
            return false;
        }

        // DELETE api/<CoffeeStoreController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
