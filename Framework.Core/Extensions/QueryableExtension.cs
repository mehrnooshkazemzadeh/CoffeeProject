using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Transactions;

namespace Framework.Core.Extensions
{
    public static class QueryableExtension
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string memberName)
        {
            var typeParams = new[] { Expression.Parameter(typeof(T), "") };

            var pi = typeof(T).GetProperty(memberName);

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    "OrderBy",
                    new[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams)));
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string memberName)
        {
            var typeParams = new[] { Expression.Parameter(typeof(T), "") };

            var pi = typeof(T).GetProperty(memberName);

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    "OrderByDescending",
                    new[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams)));
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IQueryable<T> query, string memberName)
        {
            var typeParams = new[] { Expression.Parameter(typeof(T), "") };

            var pi = typeof(T).GetProperty(memberName);

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    "ThenBy",
                    new[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams)));
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> query, string memberName)
        {
            var typeParams = new[] { Expression.Parameter(typeof(T), "") };

            var pi = typeof(T).GetProperty(memberName);

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    "ThenBy",
                    new[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams)));
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IQueryable<T> query, string memberName)
        {
            var typeParams = new[] { Expression.Parameter(typeof(T), "") };

            var pi = typeof(T).GetProperty(memberName);

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    "ThenByDescending",
                    new[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams)));
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> query, string memberName)
        {
            var typeParams = new[] { Expression.Parameter(typeof(T), "") };

            var pi = typeof(T).GetProperty(memberName);

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    "ThenByDescending",
                    new[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams)));
        }

        public static T SingleOrDefault<T>(this IQueryable<T> query, object key, PropertyInfo keyProperty)
        {
            var param = Expression.Parameter(typeof(T), "x");

            var property = Expression.MakeMemberAccess(param, keyProperty);
            var equalOperation = Expression.Equal(property, Expression.Constant(key));

            var newExp = Expression.Lambda<Func<T, bool>>(equalOperation, param).Compile();

            return query.SingleOrDefault(newExp);
        }
        public static List<T> ToListReadUncommitted<T>(this IQueryable<T> query)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions()
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }))
            {
                List<T> toReturn = query.ToList();
                scope.Complete();
                return toReturn;
            }
        }

        public static int CountReadUncommitted<T>(this IQueryable<T> query)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions()
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }))
            {
                int toReturn = query.Count();
                scope.Complete();
                return toReturn;
            }
        }


    }
}

