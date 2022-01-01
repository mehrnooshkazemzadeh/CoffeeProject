using CoffeeService.Entities;
using CoffeeService.Logic;
using Framework.Core.Logic;
using Framework.Core.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
           services.AddDbContext<CoffeeDBContext>(x=>x.UseSqlServer(Configuration.GetConnectionString("CoffeeDB")));
            services.AddTransient<ICoffeeLogic, CoffeeLogic>();
            services.AddScoped(typeof(ILogic<>), typeof(Logic<>));
            services.AddScoped(typeof(IBusinessOperations<,,>), typeof(BusinessOperations<,,>));
            services.AddScoped(typeof(IPersistenceService<>), typeof(PersistenceService<>));
            services.AddScoped<IUnitOfWork>(x => x.GetService<CoffeeDBContext>());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                     name: "DefaultApi",
                pattern: "api/{controller}/{action}/{id?}",
                     defaults: new { action = "get" });
            });
        }
    }
}
