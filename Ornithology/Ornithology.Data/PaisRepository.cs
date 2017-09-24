using Ornithology.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Data
{
    public class PaisRepository : Repository<Pais> , IPaisRepository
    {
        private IUnitOfWork _unitOfWork;

        public PaisRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
