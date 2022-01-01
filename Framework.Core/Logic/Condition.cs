using Framework.Core.Domain;
using System;
using System.Linq.Expressions;

namespace Framework.Core.Logic
{
    public class Condition<TEntity> where TEntity : EntityBase
    {
        public Expression<Func<TEntity, bool>> ConditionExpression { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }

        //public void Add(Expression<Func<TEntity,bool>> expression)
        //{
        //    Add(expression, "");
        //}
        //public void Add(Expression<Func<TEntity, bool>> expression,string name)
        //{
        //    ConditionExpression = expression;
        //    IsActive = true;
        //    Name = name;
        //}
    }
}
