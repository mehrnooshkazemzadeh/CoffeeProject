using Framework.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Core.Logic
{
    public class ConditionList<TEntity> : List<Condition<TEntity>>
        where TEntity : EntityBase
    {
        public void Add(Expression<Func<TEntity, bool>> expression, string name)
        {
            Add(new Condition<TEntity> { ConditionExpression = expression, IsActive = true, Name = name });
        }
        public void Add(Expression<Func<TEntity, bool>> expression)
        {
            Add(new Condition<TEntity> { ConditionExpression = expression, IsActive = true, Name = "" });
        }

        public void Deactive(string name)
        {
            var item = this.FirstOrDefault(x => x.Name == name);
            item.IsActive = false;
        }
        public void Active(string name)
        {
            var item = this.FirstOrDefault(x => x.Name == name);
            item.IsActive = true;
        }
        public void Toggle(string name)
        {
            var item = this.FirstOrDefault(x => x.Name == name);
            item.IsActive = !item.IsActive;
        }


    }
}
