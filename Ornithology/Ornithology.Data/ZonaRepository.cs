using Ornithology.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Data
{
    public class ZonaRepository : Repository<Zona> , IZonaRepository
    {
        private IUnitOfWork _unitOfWork;

        public ZonaRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
