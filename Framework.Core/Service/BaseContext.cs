using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Framework.Core.Domain;
using Framework.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Framework.Core.Service
{
    public class BaseContext : DbContext, IUnitOfWork
    {
        protected static List<Assembly> ConfigurationsAssemblies = new List<Assembly>();
        protected static List<Type> DynamicTypes = new List<Type>();
        protected bool LoadDynamicEntities = true;
        protected readonly IServiceProvider _serviceProvider;
        private readonly ILogger logger;

        public BaseContext()
        {
            LoadDynamicEntities = true;
          
        }
        public BaseContext(DbContextOptions<BaseContext> options, IServiceProvider serviceProvider, ILogger<BaseContext> logger) : base(options)
        {
            LoadDynamicEntities = true;
            _serviceProvider = serviceProvider;

            this.logger = logger;
        }
        protected BaseContext(DbContextOptions options, IServiceProvider serviceProvider, ILogger logger) : base(options)
        {
            LoadDynamicEntities = true;
            _serviceProvider = serviceProvider;

            this.logger = logger;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterDynamicTypes(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        private void RegisterDynamicTypes(ModelBuilder modelBuilder)
        {
            if (_serviceProvider == null) return;
            var types = _serviceProvider.GetServices(typeof(EntityBase)).Select(x => x.GetType()).ToList();
            DynamicTypes = types.Where(x => !x.IsAbstract).ToList();
            DynamicTypes.ForEach(x => modelBuilder.Entity(x));
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void RejectChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        break;
                }
            }
        }



        public int SaveChanges(bool validateOnSaveEnabled, bool autoDetect = true, bool invalidateCacheDependencies = false)
        {
            try
            {
                ApplyCorrectYeKe();
                //base.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                //base.Configuration.AutoDetectChangesEnabled = autoDetect;
                var changedEntityNames = GetChangedEntityNames();
                
                var result = base.SaveChanges();
                ChangeTracker.AutoDetectChangesEnabled = autoDetect;
                if (invalidateCacheDependencies)
                {
                    // EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
                }
                return result;
            }
            catch (Exception ex)
            {
               // logger.LogError(ex, "Context Error");
                throw;
            }
        }
        public void SaveChangesAsync(bool validateOnSaveEnabled, bool invalidateCacheDependencies = false)
        {
            try
            {
                ApplyCorrectYeKe();
                var changedEntityNames = GetChangedEntityNames();
                //base.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                base.SaveChangesAsync();
                if (invalidateCacheDependencies)
                {
                    //new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Context Error");
                throw;
            }
        }
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }


        public void SetDynamicEntities(List<Type> dynamicTypes)
        {
            if (dynamicTypes == null || LoadDynamicEntities == false) return;

            DynamicTypes.AddRange(dynamicTypes);
        }



        private string[] GetChangedEntityNames()
        {
            return this.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added ||
                            x.State == EntityState.Modified ||
                            x.State == EntityState.Deleted)
                .Select(x => x.Entity.GetType().FullName)
                .Distinct()
                .ToArray();
        }
        private void ApplyCorrectYeKe()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var item in changedEntities)
            {
                if (item.Entity == null) continue;

                var propertyInfos = item.Entity.GetType().GetProperties(
                        BindingFlags.Public | BindingFlags.Instance
                        ).Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string) && !p.GetCustomAttributes(typeof(NotMappedAttribute), false).Any());

                var pr = new PropertyReflector();

                foreach (var propertyInfo in propertyInfos)
                {
                    var propName = propertyInfo.Name;
                    var val = pr.GetValue(item.Entity, propName);
                    if (val == null) continue;
                    var newVal = val.ToString().UnifyChar();
                    if (newVal == val.ToString()) continue;
                    pr.SetValue(item.Entity, propName, newVal);
                }
            }
        }



    }
}
