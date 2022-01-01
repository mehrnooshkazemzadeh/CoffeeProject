using Coffee.APIProvider;
using CoffeeFactory.WindowsService.Models;
using CoffeeFactory.WindowsService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoffeeFactory.WindowsService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICoffeeService coffeeService;
        private readonly IAPIProvider<CoffeeTypeModel> coffeeTypeService;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, ICoffeeService coffeeService, IAPIProvider<CoffeeTypeModel> coffeeTypeService)
        {
            _logger = logger;
            coffeeService.Initialize(configuration.GetSection("ServiceAddresses").GetSection("CoffeeServiceAddress").Value);
            coffeeTypeService.Initialize(configuration.GetSection("ServiceAddresses").GetSection("CoffeeTypeServiceAddress").Value);
            this.coffeeService = coffeeService;
            this.coffeeTypeService = coffeeTypeService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {

                var coffeeTypeList = coffeeTypeService.GetAll().Result.ToArray();
                if (coffeeTypeList == null || !coffeeTypeList.Any()) throw new Exception("Threre is no coffeeType in DB");

                var todayCoffeeList = new List<CoffeeModel>();
                CoffeeTypeModel currentCoffeeType = null;
                int quantity = 0;

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(5000, stoppingToken);
                    if (currentCoffeeType == null || currentCoffeeType.QuantityInPack <= quantity)
                    {
                        if (quantity != 0)
                            Console.WriteLine($"Production of {currentCoffeeType.Title} pack finished: {DateTimeOffset.Now}");

                        int rnd = new Random().Next(coffeeTypeList.Length);
                        currentCoffeeType = coffeeTypeList[rnd];
                        Console.WriteLine($"Production of \"{currentCoffeeType.Title}\" pack started: {DateTimeOffset.Now}");
                        quantity = 0;

                    }
                    var currentCoffee = todayCoffeeList.FirstOrDefault(x => x.ProductionDate.Date == DateTime.Now.Date && x.CoffeeTypeId == currentCoffeeType.CoffeeTypeId);
                    if (currentCoffee == null)
                    {
                        currentCoffee = coffeeService.GetTodayCoffeeByCoffeeType(currentCoffeeType.CoffeeTypeId).Result;
                        if (currentCoffee == null)
                        {
                            currentCoffee = new CoffeeModel
                            {
                                CoffeeTypeId = currentCoffeeType.CoffeeTypeId,
                                ProductionDate = DateTime.Now
                            };

                        }
                    }
                    currentCoffee.Quantity += 1;
                    quantity += 1;
                    if (currentCoffee.CoffeeId != Guid.Empty)
                        coffeeService.Update(currentCoffee);
                    else
                    {
                        var res = coffeeService.Insert(currentCoffee);
                        if (res != null)
                            currentCoffee = res.Result;
                    }
                    todayCoffeeList.Add(currentCoffee);

                    Console.WriteLine($"{quantity} {currentCoffeeType.Title} producted: {DateTimeOffset.Now}");


                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                //Console.WriteLine(ex.Message);
            }
        }
    }
}
