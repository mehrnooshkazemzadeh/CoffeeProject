using Coffee.APIProvider;
using Coffee.WebApplication.Models;
using Coffee.WebApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee.WebApplication.Controllers.ControllerPanel
{
    public class CoffeeStoreController : Controller
    {
        private readonly ILogger<CoffeeStoreController> logger;
        private readonly ICoffeeService coffeeService;
        private readonly IAPIProvider<StoreModel> storeService;
        private readonly IAPIProvider<CoffeeStoreModel> coffeeStoreService;
        private readonly IStoreScheduleService storeScheduleService;

        public CoffeeStoreController(ILogger<CoffeeStoreController> logger,IConfiguration configuration, ICoffeeService coffeeService
            , IAPIProvider<StoreModel> storeService ,IAPIProvider<CoffeeStoreModel> coffeeStoreService,IStoreScheduleService storeScheduleService)
        {
            this.logger = logger;
            storeService.Initialize(configuration.GetSection("ServiceAddresses").GetSection("StoreService").Value);
            storeScheduleService.Initialize(configuration.GetSection("ServiceAddresses").GetSection("StoreScheduleService").Value);
            coffeeService.Initialize(configuration.GetSection("ServiceAddresses").GetSection("CoffeeServiceAddress").Value);
            coffeeStoreService.Initialize(configuration.GetSection("ServiceAddresses").GetSection("CoffeeStoreServiceAddress").Value);
            this.coffeeService = coffeeService;
            this.storeService = storeService;
            this.coffeeStoreService = coffeeStoreService;
            this.storeScheduleService = storeScheduleService;
        }
        public IActionResult StoreInventoryReport()
        {
            var result = coffeeStoreService.GetAll();
            return View(result.Result);
        }

        public IActionResult CoffeeList()
        {
            var coffees= coffeeService.GetCoffeeEntities().Result;
            var stores = storeService.GetAll().Result;
            var data = new CoffeeToStoreModel { CoffeeModels = coffees, Stores = stores };
            return View(data);
        }

        public IActionResult MoveToStore(CoffeeToStoreModel model)
        {
            if (ModelState.IsValid && model != null && model.CoffeeModels != null)
            {
                var entitiesToMove = model.CoffeeModels.Where(x => x.SendedPackQuantity > 0).ToList();
                var schedules = storeScheduleService.GetByStoreId(model.StoreId);
                var day = model.PostDate.Date.DayOfWeek;
                var totalHour = model.PostDate.TimeOfDay.TotalHours > 12 ? model.PostDate.TimeOfDay.TotalHours - 12 : model.PostDate.TimeOfDay.TotalHours;
                if (!schedules.Result.Any(x => x.Day == day.ToString() 
                && (x.StartTime.TimeOfDay.TotalHours <= totalHour
                && x.EndTime.TimeOfDay.TotalHours >= totalHour)))
                {
                    ModelState.AddModelError("PostDate", "Store at this time is close.");
                    model.Stores= storeService.GetAll().Result;
                    return View("CoffeeList",model);
                }

                foreach (var item in entitiesToMove)
                {
                    coffeeStoreService.Insert(new CoffeeStoreModel
                    {
                        CoffeeId =item.CoffeeId,
                        Quantity = item.SendedPackQuantity * item.CoffeeTypeModel.QuantityInPack,
                        Poststatus = 1,
                        PostDate = DateTime.Now,
                        StoreId = model.StoreId
                    });
                }
            }
            return RedirectToAction("CoffeeList");
        }
        [HttpPost]
        public IActionResult RecieveCoffees(List<CoffeeStoreModel> coffeeStoreModels)
        {
            if (ModelState.IsValid)
            {
                var lst = coffeeStoreModels.Where(x => x.Checked).ToList();
                lst.ForEach(x =>
                {
                    x.Poststatus = 2;
                    coffeeStoreService.Update(x);
                });
            }
            return RedirectToAction("StoreInventoryReport");
        }

    }
}
