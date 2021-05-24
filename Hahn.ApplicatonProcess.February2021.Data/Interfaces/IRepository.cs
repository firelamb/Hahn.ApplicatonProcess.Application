using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
        );

        Task<TEntity> GetByIdAsync(object id);
        Task InsertAsync(TEntity entity);
        void Delete(params object[] id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}