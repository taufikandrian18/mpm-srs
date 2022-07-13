using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MPMSRS.Services.Interfaces
{
    public interface IUserProfile<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void CreateBulk(List<T> entities);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
