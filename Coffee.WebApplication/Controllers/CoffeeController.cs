using Coffee.APIProvider;
using Coffee.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee.WebApplication.Controllers
{
    public class ControlPanelController : Controller
    {
        private readonly IAPIProvider<CoffeeStoreModel> coffeeStoreService;

        public ControlPanelController(IAPIProvider<CoffeeStoreModel> coffeeStoreService)
        {
            this.coffeeStoreService = coffeeStoreService;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
