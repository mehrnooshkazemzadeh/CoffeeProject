using Coffee.APIProvider;
using CoffeeFactory.WindowsService.Models;
using CoffeeFactory.WindowsService.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeFactory.WindowsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder( string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingleton(typeof(IAPIProvider<>), typeof(APIProvider<>));
                    services.AddSingleton(typeof(ICoffeeService), typeof(CoffeeService));
                });
    }
}
