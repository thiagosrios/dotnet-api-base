using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiBase.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity FindById(object id);
        TEntity FindSingle(Expression<Func<TEntity, bool>> predicate, string[] includes = null);
        IEnumerable<TEntity> FindAll();
        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, string[] includes = null);
        IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
        void Save(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
