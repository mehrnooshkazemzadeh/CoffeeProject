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
using StoreAPI.Entities;
using StoreAPI.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogestikAPI
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
            services.AddDbContext<StoreDBContext>(x => x.UseSqlServer(Configuration.GetConnectionString("CoffeeDB")));
            services.AddScoped(typeof(ILogic<>), typeof(Logic<>));
            services.AddScoped(typeof(IBusinessOperations<,,>), typeof(BusinessOperations<,,>));
            services.AddScoped(typeof(IPersistenceService<>), typeof(PersistenceService<>));
            services.AddScoped(typeof(IStoreScheduleLogic), typeof(StoreScheduleLogic));
            services.AddScoped<IUnitOfWork>(x => x.GetService<StoreDBContext>());
            services.AddControllers();

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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                     name: "DefaultApi",
                pattern: "api/{controller}/{action}/{id?}",
                     defaults: new { action = "get" });







            });

            //app.UseEndpoints(endpoints =>
            //{
            //   
            //});
        }
    }
}
