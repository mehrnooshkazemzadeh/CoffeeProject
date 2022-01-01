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

namespace Coffee.WebApplication.ControllerPanel.Controllers
{
    public class StoreController : Controller
    {
        private readonly ILogger<StoreController> logger;
        private readonly IAPIProvider<StoreModel> storeService;

        public StoreController(IConfiguration configuration,ILogger<StoreController> logger, IAPIProvider<StoreModel> storeService)
        {
            this.logger = logger;
            this.storeService = storeService;
            storeService.Initialize(configuration.GetSection("ServiceAddresses").GetSection("StoreService").Value);
        }
        public IActionResult Index()
        {
            var storeList = storeService.GetAll();
            return View("Index",storeList.Result);
        }

        public IActionResult Create(StoreModel storeModel)
        {
            return View("Create", storeModel);
        }
        [HttpPost]
        public IActionResult Save(StoreModel storeModel)
        {
            if (!ModelState.IsValid) return RedirectToAction("Create", storeModel);
            if (storeModel.StoreId == Guid.Empty)
                storeService.Insert(storeModel);
            else
                storeService.Update(storeModel);
            return View("Create", storeModel);
        }
        public IActionResult Delete( Guid id)
        {
            var result = storeService.Delete(id);
            return Index();
        }

        public IActionResult Edit(Guid id)
        {
            var storeModel = storeService.GetById("",id);
            return View("Create", storeModel.Result);
        }

    }
}
