using Framework.Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Entities
{
    public class StoreDBContext:BaseContext
    {
        private string connectionString;

        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreSchedule> StoreSchedules { get; set; }
        public StoreDBContext()
        {
            connectionString = "Server=45.83.104.117;Database=CoffeeDB;user id=sa;password=ra8G@kfhXmrrJG;MultipleActiveResultSets=true";
        }
        public StoreDBContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("CoffeeDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreSchedule>().Property<DateTime>("StartTime").HasColumnName(@"StartTime").HasColumnType(@"time").IsRequired().HasConversion(v => v.TimeOfDay, v => DateTime.Now.Date.Add(v));
            modelBuilder.Entity<StoreSchedule>().Property<DateTime>("EndTime").HasColumnName(@"EndTime").HasColumnType(@"time").IsRequired().HasConversion(v => v.TimeOfDay, v => DateTime.Now.Date.Add(v));
            base.OnModelCreating(modelBuilder);
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        
            base.OnConfiguring(optionsBuilder);
           
        }
    }
}
