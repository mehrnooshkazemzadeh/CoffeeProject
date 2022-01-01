using Mapster;
using Microsoft.EntityFrameworkCore;
using Framework.Core.Domain;
using Framework.Core.Extensions;
using Framework.Core.Service;
using Framework.Core.Tools;
using Syncfusion.XlsIO.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace Framework.Core.Logic
{
    public class BusinessOperations<TModel, TEntity, TKey> : IBusinessOperations<TModel, TEntity, TKey>
        where TModel : ModelBase<TEntity, TKey>, new()
        where TEntity : EntityBase
    {
        #region Common Properties
        public bool Cachable { get; set; }
        #endregion

        #region Delegates
        protected delegate void BeforeSaveHandler(EntityEventArgs<TEntity> entity);
        protected delegate void AfterSaveHandler(EntityEventArgs<TEntity> entity);
        protected delegate void BeforeAddHandler(EntityEventArgs<TEntity> entity);
        protected delegate void BeforeUpdateHandler(EntityEventArgs<TEntity> entity);
        protected delegate void BeforeDeleteHandler(EntityEventArgs<TEntity> entity);
        protected delegate void AfterAddHandler(EntityEventArgs<TEntity> entity);
        protected delegate void AfterUpdateHandler(EntityEventArgs<TEntity> entity);
        protected delegate void AfterDeleteHandler(EntityEventArgs<TEntity> entity);




        protected delegate void BeforeBatchSaveHandler(EntityBatchEventArgs<TEntity> entity);
        protected delegate void AfterBatchSaveHandler(EntityBatchEventArgs<TEntity> entity);
        protected delegate void BeforeBatchAddHandler(EntityBatchEventArgs<TEntity> entity);
        protected delegate void BeforeBatchUpdateHandler(EntityBatchEventArgs<TEntity> entity);
        protected delegate void BeforeBatchDeleteHandler(EntityBatchEventArgs<TEntity> entity);
        protected delegate void AfterBatchAddHandler(EntityBatchEventArgs<TEntity> entity);
        protected delegate void AfterBatchUpdateHandler(EntityBatchEventArgs<TEntity> entity);
        protected delegate void AfterBatchDeleteHandler(EntityBatchEventArgs<TEntity> entity);

        #endregion

        #region Events
        protected event BeforeSaveHandler BeforeSave;
        protected event AfterSaveHandler AfterSave;

        protected event AfterAddHandler AfterAdd;
        protected event BeforeAddHandler BeforeAdd;

        protected event BeforeUpdateHandler BeforeUpdate;
        protected event AfterUpdateHandler AfterUpdate;

        protected event AfterDeleteHandler AfterDelete;
        protected event BeforeDeleteHandler BeforeDelete;


        protected event BeforeBatchSaveHandler BeforeBatchSave;
        protected event AfterBatchSaveHandler AfterBatchSave;

        protected event AfterBatchAddHandler AfterBatchAdd;
        protected event BeforeBatchAddHandler BeforeBatchAdd;

        protected event BeforeBatchUpdateHandler BeforeBatchUpdate;
        protected event AfterBatchUpdateHandler AfterBatchUpdate;

        protected event AfterBatchDeleteHandler AfterBatchDelete;
        protected event BeforeBatchDeleteHandler BeforeBatchDelete;

        #endregion

        #region Event Handlers

        protected virtual void OnAfterBatchDelete(EntityBatchEventArgs<TEntity> entity)
        {
            var handler = AfterBatchDelete;
            handler?.Invoke(entity);
        }
        protected virtual void OnBeforeBatchDelete(EntityBatchEventArgs<TEntity> entity)
        {
            var handler = BeforeBatchDelete;
            handler?.Invoke(entity);
        }
        protected virtual void OnAfterBatchSave(EntityBatchEventArgs<TEntity> entity)
        {
            var handler = AfterBatchSave;
            handler?.Invoke(entity);
        }
        protected virtual void OnBeforeBatchAdd(EntityBatchEventArgs<TEntity> entity)
        {
            var handler = BeforeBatchAdd;
            handler?.Invoke(entity);
        }
        protected virtual void OnAfterBatchAdd(EntityBatchEventArgs<TEntity> entity)
        {
            var handler = AfterBatchAdd;
            handler?.Invoke(entity);
        }
        protected virtual void OnBeforeBatchUpdate(EntityBatchEventArgs<TEntity> entity)
        {
            var handler = BeforeBatchUpdate;
            handler?.Invoke(entity);
        }
        protected virtual void OnAfterBatchUpdate(EntityBatchEventArgs<TEntity> entity)
        {
            var handler = AfterBatchUpdate;
            handler?.Invoke(entity);
        }
        protected virtual void OnBeforeBatchSave(EntityBatchEventArgs<TEntity> entity)
        {
            var handler = BeforeBatchSave;
            handler?.Invoke(entity);
        }






        protected virtual void OnAfterDelete(EntityEventArgs<TEntity> entity)
        {
            var handler = AfterDelete;
            handler?.Invoke(entity);
        }
        protected virtual void OnBeforeDelete(EntityEventArgs<TEntity> entity)
        {
            var handler = BeforeDelete;
            handler?.Invoke(entity);
        }
        protected virtual void OnAfterSave(EntityEventArgs<TEntity> entity)
        {
            var handler = AfterSave;
            handler?.Invoke(entity);
        }
        protected virtual void OnBeforeAdd(EntityEventArgs<TEntity> entity)
        {
            var handler = BeforeAdd;
            handler?.Invoke(entity);
        }
        protected virtual void OnAfterAdd(EntityEventArgs<TEntity> entity)
        {
            var handler = AfterAdd;
            handler?.Invoke(entity);
        }
        protected virtual void OnBeforeUpdate(EntityEventArgs<TEntity> entity)
        {
            var handler = BeforeUpdate;
            handler?.Invoke(entity);
        }
        protected virtual void OnAfterUpdate(EntityEventArgs<TEntity> entity)
        {
            var handler = AfterUpdate;
            handler?.Invoke(entity);
        }
        protected virtual void OnBeforeSave(EntityEventArgs<TEntity> entity)
        {
            BeforeSave?.Invoke(entity);
        }
        #endregion

        #region Security Methods
        protected virtual bool CheckSecurityForDelete(TEntity entityToDelete, object key)
        {
            return true;
        }

        protected virtual bool CheckSecurityForBatchDelete(Expression<Func<TEntity, bool>> targets)
        {
            return true;
        }

        protected virtual bool CheckSecurityForInsert(TEntity entity)
        {
            return true;
        }
        protected virtual bool CheckSecurityForUpdate(TEntity entity)
        {
            return true;
        }
        #endregion

        #region Condition Properties
        protected ConditionList<TEntity> AdditionalConditions { get; set; }
        protected List<Expression<Func<TEntity, bool>>> SecurityConditions { get; set; }
        #endregion

        #region Service Property
        protected IPersistenceService<TEntity> Service { get; set; }
        #endregion

        #region Constractor
        public BusinessOperations(IPersistenceService<TEntity> service)
        {
            Service = service;
            AdditionalConditions = new ConditionList<TEntity>();
            SecurityConditions = new List<Expression<Func<TEntity, bool>>>();

        }
        #endregion

        #region Validation And Modification
        protected virtual TModel EntityModification(TModel item)
        {
            return item;
        }

        protected virtual bool EntityValidation(TEntity item, List<string> messages)
        {
            return true;
        }

        #endregion

        public virtual BusinessOperationResult<List<TModel>> GetAll()
        {
            return GetAll(null, null, true, null, null);

        }
       
        public virtual BusinessOperationResult<List<TModel>> GetAll(int row, int max)
        {
            return GetAll(null, row, max);
        }

        public BusinessOperationResult<List<TModel>> GetAll(int row, int max, List<SortInformation> sortInformations)
        {
            var exp = CreateExpressionByFilterDictionary(null);
            return GetData<TModel>(exp, null, row: row, max: max, sortInformation: sortInformations);

        }

        public BusinessOperationResult<List<TModel>> GetAll(int row, int max, ref long count)
        {
            return GetAll(null, row, max);

        }
        public BusinessOperationResult<List<TModel>> GetAll(int row, int max, List<SortInformation> sortInformations, List<string> includes)
        {
            var exp = CreateExpressionByFilterDictionary(null);
            return GetData<TModel>(exp, includes, row: row, max: max, sortInformation: sortInformations);
        }
        public virtual BusinessOperationResult<List<TModel>> GetAll(Dictionary<string, object> filters)
        {
            var exp = CreateExpressionByFilterDictionary(filters);
            return GetAll(exp, null, true, null, null);
        }

        public virtual BusinessOperationResult<List<TModel>> GetAll(Dictionary<string, object> filters, int row, int max)
        {
            var exp = CreateExpressionByFilterDictionary(filters);
            return GetAll(exp, null, true, row, max);
        }




        private Expression<Func<TEntity, bool>> CreateExpressionByFilterDictionary(Dictionary<string, object> filters)
        {

            if (filters == null) return null;

            Expression<Func<TEntity, bool>> exp = null;
            var param = Expression.Parameter(typeof(TEntity), "x");

            foreach (var filter in filters)
            {
                var propertyInfo = typeof(TEntity).GetProperty(filter.Key);
                var property = Expression.MakeMemberAccess(param, propertyInfo);
                var equalOperation = Expression.Equal(property, Expression.Constant(filter.Value, property.Type));
                var newExp = Expression.Lambda<Func<TEntity, bool>>(equalOperation, param);
                exp = exp == null ? newExp : exp.AndAlso(newExp);
            }


            return exp;
        }

      
        protected BusinessOperationResult<List<TModel>> GetAll(Expression<Func<TEntity, bool>> predicate,
            string orderByMember, bool orderByDescending, int? row, int? max)
        {
            return GetData<TModel>(predicate, null, orderByMember, orderByDescending, row, max);
        }
        protected BusinessOperationResult<List<TModel>> GetAll(Expression<Func<TEntity, bool>> predicate,
                string orderByMember, List<string> includes, bool orderByDescending, int? row, int? max)
        {
            return GetData<TModel>(predicate, includes, orderByMember, orderByDescending, row, max);
        }
        protected BusinessOperationResult<List<T>> GetData<T>(Expression<Func<TEntity, bool>> predicate, List<string> includes = null,
        string orderByMember = null, bool orderByDescending = true, int? row = null, int? max = null, List<SortInformation> sortInformation = null)
               where T : ModelBase<TEntity, TKey>, new()
        {
            long? count = 0;
            return GetData<T>(predicate, out count, includes, orderByMember, orderByDescending, row, max, sortInformation);

        }
        protected BusinessOperationResult<List<T>> GetData<T>(Expression<Func<TEntity, bool>> predicate, out long? count, List<string> includes = null,
     string orderByMember = null, bool orderByDescending = true, int? row = null, int? max = null, List<SortInformation> sortInformation = null)
            where T : ModelBase<TEntity, TKey>, new()
        {
            return GetData<T>(predicate, out count, includes?.ToDictionary(x => x, x => false), orderByMember, orderByDescending, row, max, sortInformation);

        }
        protected BusinessOperationResult<List<T>> GetData<T>(Expression<Func<TEntity, bool>> predicate, out long? count, Dictionary<string, bool> includes = null,
        string orderByMember = null, bool orderByDescending = true, int? row = null, int? max = null, List<SortInformation> sortInformation = null)
               where T : ModelBase<TEntity, TKey>, new()
        {
            var result = new BusinessOperationResult<List<T>>();
            try
            {
                count = 0;
                IQueryable<TEntity> rawQuery;
                IQueryable<TEntity> countQuery = null;
                var countable = false;

                if (Service == null) throw new Exception("Service is null,please check IOC");

                if (sortInformation == null) sortInformation = new List<SortInformation>();


                if (!string.IsNullOrEmpty(orderByMember))
                {
                    sortInformation.Add(

                        new SortInformation
                        {
                            OrderNumber = 1,
                            PropertyName = orderByMember,
                            SortDirection = orderByDescending ? SortDirection.Desending : SortDirection.Assending
                        }
                    );
                }


                predicate = CreateAdditionCondition(predicate);

                if (SecurityConditions != null && SecurityConditions.Any())
                {
                    foreach (var securityCondition in SecurityConditions)
                    {
                        predicate = predicate == null ? securityCondition : predicate.AndAlso(securityCondition);
                    }
                }


                if (row.HasValue && max.HasValue)
                {
                    rawQuery = Service.DeferrQuery(predicate, row.Value, max.Value, sortInformation);
                    countQuery = Service.DeferrQuery(predicate);
                    countable = true;
                }
                else
                {
                    rawQuery = Service.DeferrQuery(predicate, null, null, sortInformation, null, false);
                }

                if (includes != null && includes.Any())
                {
                    rawQuery = includes.Aggregate(rawQuery, (current, include) => include.Value ? current.Include(include.Key).DefaultIfEmpty() : current.Include(include.Key));

                }

                if (countable)
                {
                    if (Cachable)
                    {
                        // count = countQuery.Cacheable().Count();

                    }
                    else
                    {
                        count = countQuery.Count();
                    }
                    if (count == 0)
                    {
                        result.Count = 0;
                        result.SetSuccessResult(new List<T>());
                        return result;
                    }
                }

                List<T> items = null;
                //if (Cachable)
                //{
                //   items = rawQuery.Cacheable().ProjectTo<T>(BaseMapper.ConfigurationProvider).ToList();

                //}
                //else
                //{
                items = rawQuery.ProjectToType<T>().ToList();

                //}
              result.Count = countable ? count : items.Count();
                result.SetSuccessResult(items);

                //result.Count = count;
            }
            catch (Exception ex)
            {
                var exMessage = ExceptionParser.Parse(ex);

                //var st = new StackTrace(ex, true);
                //// Get the top stack frame
                //var frame = st.GetFrame(0);
                //// Get the line number from the stack frame
                //var line = frame.GetFileLineNumber();
                //frame = st.GetFrame(1);

                //Core.Utility.EventLogger.Log(exMessage, 100, "Application", EventLogEntryType.Error);

                //Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("GetData Error : " + frame.GetFileName(), ex));
                result.SetErrorMessage(exMessage, null);
                //throw ex;
                count = 0;
            }
            return result;
        }

        private Expression<Func<TEntity, bool>> CreateAdditionCondition(Expression<Func<TEntity, bool>> predicate)
        {
            if (AdditionalConditions == null) return null;
            var temp = AdditionalConditions.Where(x => x.IsActive).ToList();
            foreach (var additionalCondition in temp.Select(x => x.ConditionExpression).ToList())
            {
                predicate = predicate == null ? additionalCondition : predicate.AndAlso(additionalCondition);
            }

            return predicate;
        }

        public virtual BusinessOperationResult<TModel> GetRow(object key)
        {
            return GetRow(key, null);
        }

        public virtual BusinessOperationResult<TModel> GetRow(object key, List<string> includes)
        {
            var result = new BusinessOperationResult<TModel>();
            try
            {
                var model = new TModel();
                var query = Service.DeferrQuery();
                Expression<Func<TEntity, bool>> predicate = null;

                predicate = CreateAdditionCondition(predicate);

                if (SecurityConditions != null && SecurityConditions.Any())
                {
                    foreach (var securityCondition in SecurityConditions)
                    {
                        predicate = predicate == null ? securityCondition : predicate.AndAlso(securityCondition);
                    }
                }
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                if (includes != null && includes.Any())
                {
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
                }



                TEntity item;
                if (Cachable)
                {
                    item = query.SingleOrDefault(key, Service.GetKeyProperty());
                }
                else
                {
                    item = query.SingleOrDefault(key, Service.GetKeyProperty());
                }

            var data = item.Adapt<TModel>();
                result.SetSuccessResult(data);
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                //result.SetErrorMessage(Properties.Resources.CommonError);
                throw ex;
            }
            return result;
        }
        public virtual BusinessOperationResult<TModel> First(Expression<Func<TEntity, bool>> predicate, List<string> includes = null)
        {
            var result = new BusinessOperationResult<TModel>();
            try
            {
                var model = new TModel();
                var query = Service.DeferrQuery();


                predicate = CreateAdditionCondition(predicate);

                if (SecurityConditions != null && SecurityConditions.Any())
                {
                    foreach (var securityCondition in SecurityConditions)
                    {
                        predicate = predicate == null ? securityCondition : predicate.AndAlso(securityCondition);
                    }
                }
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                if (includes != null && includes.Any())
                {
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
                }


                TModel data;
                //if (Cachable)
                //{
                //    data = query.Cacheable().ProjectTo<TModel>(BaseMapper.ConfigurationProvider).FirstOrDefault();

                //}
                //else
                //{
                data = query.ProjectToType<TModel>().FirstOrDefault();

                //}
                result.SetSuccessResult(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public virtual BusinessOperationResult<T> GetFirst<T>(Expression<Func<TEntity, bool>> predicate, List<string> includes = null)
            where T : ModelBase<TEntity, TKey>, new()
        {
            var result = new BusinessOperationResult<T>();
            try
            {
                var model = new T();
                var query = Service.DeferrQuery();



                predicate = CreateAdditionCondition(predicate);

                if (SecurityConditions != null && SecurityConditions.Any())
                {
                    foreach (var securityCondition in SecurityConditions)
                    {
                        predicate = predicate == null ? securityCondition : predicate.AndAlso(securityCondition);
                    }
                }
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                if (includes != null && includes.Any())
                {
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
                }

                T data;
                //if (Cachable)
                //{
                //    data = query.Cacheable().ProjectTo<T>(BaseMapper.ConfigurationProvider).FirstOrDefault();

                //}
                //else
                //{
                data = query.ProjectToType<T>().FirstOrDefault();

                //}

                result.SetSuccessResult(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        public BusinessOperationResult<bool> Delete(object key)
        {
            var result = new BusinessOperationResult<bool>();

            using (var scope = new TransactionScope())
            {

                try
                {
                    var entityToDelete = Service.GetRow(key);

                    if (!CheckSecurityForDelete(entityToDelete, key))
                    {

                        result = result.SetSingleMessage("Security error on delete entity", OperationResultStatus.Error, false);
                        return result;
                    }

                    var args = new EntityEventArgs<TEntity> { NewEntity = entityToDelete, OldEntity = entityToDelete, EntityAction = EntityAction.Delete };
                    OnBeforeDelete(args);
                    OnBeforeSave(args);
                    Service.Delete(entityToDelete);
                    Service.Save();
                    OnAfterDelete(args);
                    OnAfterSave(args);
                    scope.Complete();
                    result.SetSingleMessage("", OperationResultStatus.Successful, true);
                }
                catch (Exception ex)
                {
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    var message = ExceptionParser.Parse(ex);
                    result = result.SetSingleMessage(message, OperationResultStatus.Error, false);
                    //throw ex;

                }
            }
            return result;
        }
        public BusinessOperationResult<bool> Delete(List<object> keys)
        {

            //query.Contains(x => keys.Contains(x.Key));


            var param = Expression.Parameter(typeof(TEntity), "x");

            var property = Expression.MakeMemberAccess(param, Service.GetKeyProperty());

            var containsMethod = (new List<TEntity>()).GetType().GetMethod("Contains");
            var containsOperation = Expression.Call(property, containsMethod, Expression.Constant(keys));

            var newExp = Expression.Lambda<Func<TEntity, bool>>(containsOperation, param).Compile();



            return Delete(newExp);
        }
        public BusinessOperationResult<bool> Delete(Expression<Func<TEntity, bool>> targets)
        {
            var result = new BusinessOperationResult<bool>();

            using (var scope = new TransactionScope())
            {

                try
                {

                    if (!CheckSecurityForBatchDelete(targets))
                    {

                        result = result.SetSingleMessage("Security error on remove entity", OperationResultStatus.Error, false);
                        return result;
                    }

                    var args = new EntityBatchEventArgs<TEntity> { Predicate = targets, EntityAction = EntityAction.Delete };


                    OnBeforeBatchDelete(args);
                    OnBeforeBatchSave(args);

                    Service.Delete(targets);
                    Service.Save();

                    OnAfterBatchDelete(args);
                    OnAfterBatchSave(args);

                    scope.Complete();
                    result.SetSingleMessage("", OperationResultStatus.Successful, true);
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
            return result;
        }


        public BusinessOperationResult<TModel> AddNew(TModel newItem)
        {
            var result = new BusinessOperationResult<TModel>();
            var validationMessages = new List<string>();
            using var scope = new TransactionScope();
            try
            {
                newItem = EntityModification(newItem);

                var item = newItem.GetDomain();

                if (!CheckSecurityForInsert(item))
                {
                    result = result.SetSingleMessage("Security error on add new entity", OperationResultStatus.Error, null);
                    return result;
                }
                if (!EntityValidation(item, validationMessages))
                {
                    result.SetSingleMessage("Validation error", OperationResultStatus.Error, null);
                    result.Messages.AddRange(validationMessages);
                    return result;
                }
                var args = new EntityEventArgs<TEntity> { NewEntity = item, OldEntity = item, EntityAction = EntityAction.Insert };
                OnBeforeAdd(args);
                OnBeforeSave(args);
                var entity = Service.Add(args.NewEntity);
                Service.Save();
                args.NewEntity = entity.Entity;
                OnAfterAdd(args);
                OnAfterSave(args);
                scope.Complete();
                var model = new TModel();
                model = entity.Entity.Adapt<TModel>();
                result.SetSingleMessage("", OperationResultStatus.Successful, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        public virtual BusinessOperationResult<bool> Update(TModel item)
        {
            var validationMessages = new List<string>();
            var result = new BusinessOperationResult<bool>();
            using (var scope = new TransactionScope())
            {
                try
                {
                    item = EntityModification(item);

                    var entity = item.GetDomain();

                    if (!CheckSecurityForUpdate(entity))
                    {
                        result = result.SetSingleMessage("Security Error on update item.", OperationResultStatus.Error, false);
                        return result;
                    }


                    if (EntityValidation(entity, validationMessages))
                    {
                        var args = new EntityEventArgs<TEntity> { NewEntity = entity, OldEntity = entity, EntityAction = EntityAction.Update };
                        OnBeforeUpdate(args);
                        OnBeforeSave(args);
                        
                        Service.Update(args.NewEntity);
                        Service.Save(false);

                        OnAfterUpdate(args);
                        OnAfterSave(args);

                        scope.Complete();
                        result.SetSingleMessage("", OperationResultStatus.Successful, true);
                    }
                    else
                    {
                        result.SetSingleMessage("Validation error", OperationResultStatus.Error, false);
                        result.Messages.AddRange(validationMessages);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
            return result;
        }

        public BusinessOperationResult<long> Count()
        {
            var result = new BusinessOperationResult<long>();

            result.SetSuccessResult(Service.GetCount());

            return result;
        }
        public BusinessOperationResult<long> Count(Expression<Func<TEntity, bool>> predicate)
        {
            var result = new BusinessOperationResult<long>();
            var query = Service.DeferrQuery();

            predicate = CreateAdditionCondition(predicate);

            if (SecurityConditions != null && SecurityConditions.Any())
            {
                foreach (var securityCondition in SecurityConditions)
                {
                    predicate = predicate == null ? securityCondition : predicate.AndAlso(securityCondition);
                }
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            //if (Cachable)
            //{
            //    result.SetSuccessResult(query.Cacheable().Count());

            //}
            //else
            //{
            result.SetSuccessResult(query.Count());

            //}

            return result;
        }

        public BusinessOperationResult<List<TModel>> GetAll(Dictionary<string, object> filters, int row, int max, List<SortInformation> sortInformations)
        {
            var exp = CreateExpressionByFilterDictionary(filters);
            return GetData<TModel>(exp, null, orderByDescending: true, row: row, max: max, sortInformation: sortInformations);
        }

        public List<BusinessOperationResult<bool>> Update(List<TModel> items)
        {
                var result = new List<BusinessOperationResult<bool>>();
                using (var scope = new TransactionScope())
                {
                    foreach (var item in items)
                    {
                        result.Add(Update(item));
                    }
                scope.Complete();
                }
                return result;
            
        }

       
    }
}
