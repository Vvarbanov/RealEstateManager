using System;
using System.Linq;
using System.Linq.Expressions;

namespace RealEstateManager.Repository.Interfaces
{
    public interface ISet<TEntity, in TPrimaryKey, in TInsertData, in TUpdateData>
        where TEntity : class
        where TPrimaryKey : struct
        where TInsertData : class
        where TUpdateData : class
    {
        TEntity Insert(TInsertData data);

        TEntity GetById(TPrimaryKey id, string includeProperties = null);

        IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null);

        void Update(TPrimaryKey id, TUpdateData data);

        void Delete(TPrimaryKey id);
    }
}
