using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ornithology.Services
{
    public interface IServiceAsNoTracking<TEntity> where TEntity : class
    {
        
        Task<List<TEntity>> ListarAsyncFilteredAsNoTracking(Expression<Func<TEntity, bool>> filter, params string[] include);
        

        Task<List<TEntity>> ListarAsyncAsNoTracking();
    }
}