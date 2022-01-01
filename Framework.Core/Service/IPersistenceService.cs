using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Framework.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Framework.Core.Service
{
    public interface IPersistenceService<T>
        where T : EntityBase
    {
        bool IsDisposed { get; }
        bool CastToDerivedType { get; set; }
        DbSet<T> Entities { get; }
        PropertyInfo GetKeyProperty();
        DbSet<TEntity> GetNestedEntity<TEntity>() where TEntity : EntityBase;

        void MarkAsNewEntity(T entity);
        void MarkAsNewEntity(object entity);
        void MarkAsRemovedEntity(T entity);
        void MarkAsRemovedEntity(object entity);

        #region DefferQUery
        IQueryable<T> DeferrQuery();
        IQueryable<T> DeferrQuery(int start, int max);
        IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate);
        IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int start, int max);
        IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int start, int max, List<string> includePaths);
        IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int start, int max, List<SortInformation> sortInformation);
        IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, bool withTracking);
        IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int start, int max, bool withTracking);
        IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int start, int max, List<string> includePaths, bool withTracking);
        IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int start, int max, List<SortInformation> sortInformation, bool withTracking);
        IQueryable<T> DeferrQuery(Expression<Func<T, bool>> predicate, int? start, int? max, List<SortInformation> sortInformation, List<string> includePaths, bool withTracking);
        #endregion

        #region Count
        long GetCount();
        long GetCount(bool withTracking);
        long GetCount(Expression<Func<T, bool>> predicate);
        long GetCount(Expression<Func<T, bool>> predicate, bool withTracking);
        long GetCount(Expression<Func<T, bool>> predicate, bool withTracking, List<string> includePaths);
        #endregion

        #region Get All
        IList<T> GetAll();
        IList<T> GetAll(bool withTracking);
        IList<T> GetAll(Expression<Func<T, bool>> predicate);
        IList<T> GetAll(Expression<Func<T, bool>> predicate, bool withTracking);
        IList<T> GetAll(int? row, int? max);
        IList<T> GetAll(Expression<Func<T, bool>> predicate, int? row, int? max);
        IList<T> GetAll(int? start, int? max, bool withTracking);
        IList<T> GetAll(Expression<Func<T, bool>> predicate, int? start, int? max, bool withTracking);
        IList<T> GetAll(Expression<Func<T, bool>> predicate, int? start, int? max, bool withTracking, List<SortInformation> sortInformation);
        IList<T> GetAll(Expression<Func<T, bool>> predicate, int? start, int? max, bool withTracking, List<SortInformation> sortInformation, List<string> includePaths);

        #endregion

        #region First And Single
        T First();
        T First(bool withTracking);
        T First(Expression<Func<T, bool>> predicate);
        T First(Expression<Func<T, bool>> predicate, bool withTracking);
        T Single();
        T Single(bool withTracking);
        T Single(Expression<Func<T, bool>> predicate);
        T Single(Expression<Func<T, bool>> predicate, bool withTracking);
        #endregion

        #region Get All With Count
        IList<T> GetAllWithCount(out long count);
        IList<T> GetAllWithCount(bool withTracking, out long count);
        IList<T> GetAllWithCount(Expression<Func<T, bool>> predicate, out long count);
        IList<T> GetAllWithCount(Expression<Func<T, bool>> predicate, bool withTracking, out long count);
        IList<T> GetAllWithCount(int? row, int? max, out long count);
        IList<T> GetAllWithCount(Expression<Func<T, bool>> predicate, int? row, int? max, out long count);
        IList<T> GetAllWithCount(int? start, int? max, bool withTracking, out long count);

        IList<T> GetAllWithCount(Expression<Func<T, bool>> predicate, int? start, int? max, bool withTracking,
            out long count);

        IList<T> GetAllWithCount(Expression<Func<T, bool>> predicate, int? start, int? max, bool withTracking,
            List<SortInformation> sortInformation, out long count);

        IList<T> GetAllWithCount(Expression<Func<T, bool>> predicate, int? start, int? max, bool withTracking,
            List<SortInformation> sortInformation, List<string> includePaths, out long count);

        #endregion

        #region GetRow
        T GetRow(object key);
        T GetRow(object key, bool loadFromCache);
        T GetRow(Expression<Func<T, bool>> predicate);
        T GetRow(Expression<Func<T, bool>> predicate, bool withTracking);
        #endregion

        #region Add
        EntityEntry<T> Add(T entity);
        #endregion

        #region Delete
        void Delete(T entity);
        void Delete(object key);
        void Delete(Expression<Func<T, bool>> predicate);
        #endregion

        #region Update
        void Update(T entity);
        void Update(List<T> entities);
        void Update(Expression<Func<T, bool>> predicate, T newEntity);
        void Update(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> newEntity);
        #endregion

        #region Save
        int Save(bool validateEntity = true, bool autoDetect = true);
        void SaveAsync(bool validateOnSaveEnabled = true);
        #endregion

    }
}
