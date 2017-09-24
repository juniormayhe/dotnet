using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }

        IEnumerable<TEntity> GetAll();

        void Add(TEntity item);

        void AddRange(IList<TEntity> items);

        void Delete(TEntity item);

        void DeleteWhere(Expression<Func<TEntity, bool>> where);

        void Modify(TEntity item);

        void ModifyRange(IList<TEntity> items);

        IEnumerable<TEntity> GetFilteredElements(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> GetFilteredElementsAsNoTracking(Expression<Func<TEntity, bool>> filter);

        Task<List<TEntity>> GetFilteredAsyncAsNoTracking();

        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> where);

        Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);

        Task<List<TEntity>> GetFilteredAsyncAsNoTracking(Expression<Func<TEntity, bool>> filter, params string[] include);
    }
}
