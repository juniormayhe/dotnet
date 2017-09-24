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
    public class ZonaServices : IZonaServices
    {
        private IZonaRepository _zonaRepository;
        

        public ZonaServices(IZonaRepository zonaRepository)
        {
            _zonaRepository = zonaRepository;
            
        }

        public async Task<List<Zona>> ListarAsyncAsNoTracking()
        {
            return await _zonaRepository.GetFilteredAsyncAsNoTracking();
        }

        public async Task<List<Zona>> ListarAsyncFilteredAsNoTracking(Expression<Func<Zona, bool>> filter, params string[] include)
        {
            return await _zonaRepository.GetFilteredAsyncAsNoTracking(filter, include);
        }

        
    }
}
