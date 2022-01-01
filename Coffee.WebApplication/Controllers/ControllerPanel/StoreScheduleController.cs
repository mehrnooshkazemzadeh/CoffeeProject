using Coffee.WebApplication.Models;
using Coffee.WebApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee.WebApplication.ControllerPanel.Controllers
{
    public class StoreScheduleController : Controller
    {
        private readonly IStoreScheduleService storeScheduleService;

        public StoreScheduleController(IConfiguration configuration , IStoreScheduleService storeScheduleService)
        {
            storeScheduleService.Initialize(configuration.GetSection("ServiceAddresses").GetSection("StoreScheduleService").Value );
            this.storeScheduleService = storeScheduleService;
        }
        public IActionResult Index( Guid id)
        {
            var schedules = storeScheduleService.GetByStoreId(id);
            return View("Index", schedules.Result);
        }

        public IActionResult Create(StoreScheduleModel storeScheduleModel, Guid id)
        {
            if (storeScheduleModel == null) storeScheduleModel = new StoreScheduleModel();
            storeScheduleModel.StoreId = id;
            return View("Create", storeScheduleModel);
        }
        [HttpPost]
        public IActionResult Save( StoreScheduleModel storeScheduleModel, Guid id)
        {
            storeScheduleModel.StoreId = id;
            var actionName = storeScheduleModel.StoreScheduleId == Guid.Empty ? "Create" : "Edit";
            if (ModelState.IsValid)
            {
                if (storeScheduleModel.StoreScheduleId == Guid.Empty)
                    storeScheduleService.Insert(storeScheduleModel);

                else
                {
                    storeScheduleService.Update(storeScheduleModel);
                    id = storeScheduleModel.StoreScheduleId;
                }

            }
            return RedirectToAction(actionName, new { id = id  });

        }
        public IActionResult Delete(Guid storeId, Guid id)
        {
            var result = storeScheduleService.Delete(id);
            return RedirectToAction("Index",new { id = storeId });
        }

        public IActionResult Edit( Guid id)
        {
            var storeScheduleModel = storeScheduleService.GetById("GetById",id);
            return View("Create", storeScheduleModel.Result);
        }
    }
}
