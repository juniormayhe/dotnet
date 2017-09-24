using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ornithology.Services
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<List<TEntity>> ListarAsync();
        Task<TEntity> FindAsync(string codigo);
    }
}