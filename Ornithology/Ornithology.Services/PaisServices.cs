using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ornithology.Entity;
using Ornithology.Data;
using Ornithology.Domain;

namespace Ornithology.Services
{
    public class PaisServices : IPaisServices
    {
        private IPaisRepository _paisRepository;
        

        public PaisServices(IPaisRepository paisRepository)
        {
            _paisRepository = paisRepository;
            
        }

        public async Task<Pais> FindAsync(string codigo)
        {
            return await _paisRepository.GetFirstAsync(x => x.Codigo == codigo);
        }

        public async Task<List<Pais>> ListarAsync()
        {
            return await _paisRepository.GetAllAsync();
        }
        
        public async Task<List<Pais>> ListarAsyncFilteredAsNoTracking(Expression<Func<Pais, bool>> filter, params string[] include)
        {
            return await _paisRepository.GetFilteredAsyncAsNoTracking(filter, include);
        }

        public async Task<List<Pais>> ListarAsyncAsNoTracking()
        {
            return await _paisRepository.GetFilteredAsyncAsNoTracking();
        }
        
    }
}
