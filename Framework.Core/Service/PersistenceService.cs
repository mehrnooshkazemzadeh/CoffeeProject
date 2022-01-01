using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Framework.Core.Domain;
using Framework.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Framework.Core.Service
{
    public class PersistenceService<T> : IPersistenceService<T> where T : EntityBase
    {
        private readonly ILogger logger;

        public bool IsDisposed { get; private set; }

        public bool CastToDerivedType { get; set; }

        protected IUnitOfWork UnitOfWork { get; set; }
        public DbSet<T> Entities { get; private set; }


        public PersistenceService(IUnitOfWork unitOfWork, ILogger<T> logger)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Entities = UnitOfWork.Set<T>();

        }
        public DbSet<TEntity> GetNestedEntity<TEntity>() where TEntity : EntityBase
        {
            return UnitOfWork.Set<TEntity>();
        }

        private IQueryable<T> GetQueryableEntity()
        {
            IQueryable<T> query = Entities;
            if (CastToDerivedType)
            {
                query = query.OfType<T>();
            }
            return query;
        }


        public PropertyInfo GetKeyProperty()
        {
            var properties = typeof(T).GetProperties();
            var className = typeof(T).Name;
            var member = properties.FirstOrDefault(x => (x.GetCustomAttribute(typeof(KeyAttribute), true) != null) || x.Name == $"{className}Id" || x.Name == $"{className}ID");
            if (member == null)
            {
                throw new Exception("Property KEY notFound");
            }
            return member;
        }



        #region Implementation of IPartikanService<T>
        public IQueryable<T> DeferrQuery()
        {
            return GetQueryableEntity();

        }

        public IQueryable<T> DeferrQuery(int start, int max)
        {
            return DeferrQuery(null, start, max, false, null, null);

        }
        public virtual IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate)
        {
            return DeferrQuery(predicate, null, null, false, null, null);
        }

        public IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int start, int max)
        {
            return DeferrQuery(predicate, start, max, false, null, null);

        }

        public IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int start, int max, List<string> includePaths)
        {
            return DeferrQuery(predicate, start, max, false, null, includePaths);
        }

        public IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int start, int max, List<SortInformation> sortInformation)
        {
            return DeferrQuery(predicate, start, max, false, sortInformation, null);

        }

        public virtual IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, bool withTracking)
        {
            return DeferrQuery(predicate, null, null, withTracking, null, null);
        }

        public IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int start, int max, bool withTracking)
        {
            return DeferrQuery(predicate, start, max, withTracking, null, null);

        }

        public IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int start, int max, List<string> includePaths, bool withTracking)
        {
            return DeferrQuery(predicate, start, max, withTracking, null, includePaths);

        }

        public IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int start, int max, List<SortInformation> sortInformation, bool withTracking)
        {
            return DeferrQuery(predicate, start, max, withTracking, sortInformation, null);

        }

        public IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int? start, int? max, List<SortInformation> sortInformation, List<string> includePaths, bool withTracking)
        {
            return DeferrQuery(predicate, start, max, withTracking, sortInformation, includePaths);
        }

        protected virtual IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int? start, int? max,
            bool withTracking, List<SortInformation> sortInformation, List<string> includePaths)
        {
            try
            {

                var query = GetQueryableEntity();

                query = withTracking ? query : query.AsNoTracking();

                if (includePaths != null)
                {
                    foreach (var includePath in includePaths)
                    {
                        query.Include(includePath);
                    }
                }

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
                if (sortInformation != null && sortInformation.Any())
                {
                    var firstOrder = true;
                    foreach (var sort in sortInformation.OrderBy(x => x.OrderNumber).ToList())
                    {
                        if (firstOrder)
                        {
                            query = sort.SortDirection == SortDirection.Assending
                                ? query.OrderBy(sort.PropertyName)
                                : query.OrderByDescending(sort.PropertyName);

                            firstOrder = false;
                            continue;
                        }
                        query = sort.SortDirection == SortDirection.Assending
                            ? query.ThenBy(sort.PropertyName)
                            : query.ThenByDescending(sort.PropertyName);
                    }
                }
                else
                {
                    var keyProperty = GetKeyProperty();
                    query = query.OrderBy(keyProperty.Name);
                }

                query = query.Skip(start ?? 0);
                if (max.HasValue)
                {
                    query = query.Take(max.Value);
                }
                return query;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Persistence Service Deffer Query");
                throw;
            }
        }

        public virtual long GetCount()
        {
            return GetCount(null, false, null);
        }

        public virtual long GetCount(bool withTracking)
        {
            return GetCount(null, withTracking, null);
        }

        public virtual long GetCount(Expression<Func<T, bool>> predicate)
        {
            return GetCount(predicate, false, null);
        }

        public virtual long GetCount(Expression<Func<T, bool>> predicate, bool withTracking)
        {
            return GetCount(predicate, withTracking, null);
        }

        public virtual long GetCount(Expression<Func<T, bool>> predicate, bool withTracking, List<string> includePaths)
        {
            var query = DeferrQuery(predicate, null, null, withTracking, null, includePaths);
            return query.LongCount();
        }

        public virtual IList<T> GetAll()
        {
            return GetAll(null, null, null, false, null, null);
        }

        public virtual IList<T> GetAll(bool withTracking)
        {
            return GetAll(null, null, null, withTracking, null, null);
        }

        public virtual IList<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GetAll(predicate, null, null, false, null, null);
        }

        public virtual IList<T> GetAll(Expression<Func<T, bool>> predicate, bool withTracking)
        {
            return GetAll(predicate, null, null, withTracking, null, null);
        }

        public virtual IList<T> GetAll(int? row, int? max)
        {
            return GetAll(null, row, max, false, null, null);
        }

        public virtual IList<T> GetAll(Expression<Func<T, bool>> predicate, int? row, int? max)
        {
            return GetAll(predicate, row, max, false, null, null);
        }

        public virtual IList<T> GetAll(int? start, int? max, bool withTracking)
        {
            return GetAll(null, start, max, withTracking, null, null);
        }

        public virtual IList<T> GetAll(Expression<Func<T, bool>> predicate, int? start, int? max, bool withTracking)
        {
            return GetAll(predicate, start, max, withTracking, null, null);
        }

        public virtual IList<T> GetAll(Expression<Func<T, bool>> predicate, int? start, int? max, bool withTracking, List<SortInformation> sortInformation)
        {
            return GetAll(predicate, start, max, withTracking, sortInformation, null);
        }

        public virtual IList<T> GetAll(Expression<Func<T, bool>> predicate, int? start, int? max, bool withTracking, List<SortInformation> sortInformation, List<string> includePaths)
        {
            var query = DeferrQuery(predicate, start, max, withTracking, sortInformation, includePaths);
            return query.ToList();
        }

        public T First()
        {
            return DeferrQuery().First();
        }

        public T First(bool withTracking)
        {
            return DeferrQuery(null, withTracking).First();

        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return DeferrQuery(predicate).First();

        }

        public T First(Expression<Func<T, bool>> predicate, bool withTracking)
        {
            return DeferrQuery(predicate, withTracking).First();

        }

        public T Single()
        {
            return DeferrQuery().Single();

        }

        public T Single(bool withTracking)
        {
            return DeferrQuery(null, withTracking).Single();

        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return DeferrQuery(predicate).Single();

        }

        public T Single(Expression<Func<T, bool>> predicate, bool withTracking)
        {
            return DeferrQuery(null, withTracking).Single();

        }

        public virtual IList<T> GetAllWithCount(out long count)
        {
            return GetAllWithCount(null, null, null, false, null, null, out count);
        }

        public virtual IList<T> GetAllWithCount(bool withTracking, out long count)
        {
            return GetAllWithCount(null, null, null, withTracking, null, null, out count);
        }

        public virtual IList<T> GetAllWithCount(Expression<Func<T, bool>> predicate, out long count)
        {
            return GetAllWithCount(predicate, null, null, false, null, null, out count);
        }

        public virtual IList<T> GetAllWithCount(Expression<Func<T, bool>> predicate, bool withTracking, out long count)
        {
            return GetAllWithCount(predicate, null, null, withTracking, null, null, out count);
        }

        public virtual IList<T> GetAllWithCount(int? row, int? max, out long count)
        {
            return GetAllWithCount(null, row, max, false, null, null, out count);
        }

        public virtual IList<T> GetAllWithCount(Expression<Func<T, bool>> predicate, int? row, int? max, out long count)
        {
            return GetAllWithCount(predicate, row, max, false, null, null, out count);
        }

        public virtual IList<T> GetAllWithCount(int? start, int? max, bool withTracking, out long count)
        {
            return GetAllWithCount(null, start, max, withTracking, null, null, out count);
        }

        public virtual IList<T> GetAllWithCount(Expression<Func<T, bool>> predicate, int? start, int? max, bool withTracking, out long count)
        {
            return GetAllWithCount(predicate, start, max, withTracking, null, null, out count);
        }

        public virtual IList<T> GetAllWithCount(Expression<Func<T, bool>> predicate, int? start, int? max, bool withTracking, List<SortInformation> sortInformation, out long count)
        {
            return GetAllWithCount(predicate, start, max, withTracking, sortInformation, null, out count);
        }

        public virtual IList<T> GetAllWithCount(Expression<Func<T, bool>> predicate, int? start, int? max, bool withTracking, List<SortInformation> sortInformation, List<string> includePaths, out long count)
        {
            var query = DeferrQuery(predicate, start, max, withTracking, sortInformation, includePaths);
            count = query.LongCount();
            return query.ToList();
        }

        public virtual T GetRow(object key)
        {
            return Entities.Find(key);
        }

        public virtual T GetRow(object key, bool loadFromCache)
        {
            var query = GetQueryableEntity();
            return loadFromCache ? Entities.Find(key) : query.SingleOrDefault(key, GetKeyProperty());
        }

        public virtual T GetRow(Expression<Func<T, bool>> predicate, bool withTracking)
        {
            var query = DeferrQuery(predicate, withTracking);
            return query.FirstOrDefault(predicate);
        }

        public virtual T GetRow(Expression<Func<T, bool>> predicate)
        {
            return GetRow(predicate, false);
        }


        public virtual EntityEntry<T> Add(T entity)
        {
            return Entities.Add(entity);
        }



        public virtual void Delete(T entity)
        {
            Entities.Remove(entity);
        }

        public virtual void Delete(object key)
        {
            var entity = GetRow(key);
            if (entity != null)
                Delete(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
            //Entities.Where(predicate).Delete();
        }



        private void EntityUpdate(object entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            //var t = typeof(IPersistenceService<>);
            //var gt = t.MakeGenericType(entity.GetType());
            //var service = StructureMapObjectFactory.Container.GetInstance(gt);
            //var m = service.GetType().GetMethod("Update", new[] { entity.GetType() });
            //m.Invoke(service, new object[] { entity });
            throw new NotImplementedException();

        }
        public virtual void Update(T entity)
        {
            try
            {
                var entry = UnitOfWork.Entry(entity);
                var query = UnitOfWork.Set<T>();



                if (entry.State == EntityState.Detached)
                {
                    var attachedEntity = query.Local.SingleOrDefault(e => e.Key.ToString() == entity.Key.ToString());

                    if (attachedEntity != null)
                    {
                        var attachedEntry = UnitOfWork.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(entity);
                    }
                    else
                    {
                        if (entity.ModifiedProperties == null || !entity.ModifiedProperties.Any())
                        {
                            entry.State = EntityState.Modified;
                        }
                        else
                        {
                            query.Attach(entity);
                            MarkAsModified(entry, entity.ModifiedProperties.ToList());
                        }

                        if (entity.ModifiedNavigateProperties != null && entity.ModifiedNavigateProperties.Any())
                        {
                            foreach (var subEntity in entity.ModifiedNavigateProperties)
                            {
                                var inner = entity.GetType().GetProperty(subEntity);
                                if (inner != null)
                                {
                                    var value = inner.GetValue(entity);
                                    EntityUpdate(value);
                                }
                            }
                        }
                    }

                }
                else
                {
                    query.Attach(entity);
                    UnitOfWork.Entry(entity).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Context Error");
                throw;
            }
        }



        private static void MarkAsModified(EntityEntry<T> entry, IEnumerable<string> modifiedItems)
        {
            foreach (var property in modifiedItems)
            {
                entry.Property(property).IsModified = true;
            }
        }

        public void MarkAsNewEntity(T entity)
        {
            var entry = UnitOfWork.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
        }
        public void MarkAsNewEntity(object entity)

        {
            var entry = UnitOfWork.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
        }
        public void MarkAsRemovedEntity(T entity)
        {
            var entry = UnitOfWork.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Deleted;
            }
        }
        public void MarkAsRemovedEntity(object entity)
        {
            var entry = UnitOfWork.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Deleted;
            }
        }

        public void Update(Expression<Func<T, bool>> predicate, T newEntity)
        {
            throw new NotImplementedException();

        }
        public void Update(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> newEntity)
        {
            throw new NotImplementedException();

        }
        public void Update(List<T> entities)
        {
            throw new NotImplementedException();
        }


        public int Save(bool validateOnSaveEnabled = true, bool autoDetect = true)
        {
            return UnitOfWork.SaveChanges(validateOnSaveEnabled, autoDetect);
        }

        public void SaveAsync(bool validateOnSaveEnabled = true)
        {
            UnitOfWork.SaveChangesAsync(validateOnSaveEnabled);
        }


        #endregion Implementation of IPartikanService<T>

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            if (disposing)
            {
                UnitOfWork.RejectChanges();
            }

            IsDisposed = true;

        }

        ~PersistenceService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

