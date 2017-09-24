using Ornithology.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Data
{
    public class AveRepository : Repository<Ave> , IAveRepository
    {
        private IUnitOfWork _unitOfWork;
       
        public AveRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public override void Modify(Ave item) {
        //    base.Modify(item);
        //    foreach (var avePais in item.AvesPais)
        //    {
        //        //_unitOfWork.SetModified<AvePais>(avePais);
                
        //    }
        //}

        public void DeleteCascade(string codigoAve)
        {
            
            IDbSet<Ave> dbSet = _unitOfWork.CreateSet<Ave>();

            var entity = dbSet.Find(codigoAve);

            if (entity == null)
            {
                throw new ObjectNotFoundException("Ave");
            }

            EliminarAvesPais(codigoAve);
                
                
            dbSet.Remove(entity);
            
        }

        private void EliminarAvesPais(string codigoAve)
        {
            IDbSet<AvePais> dbSet = _unitOfWork.CreateSet<AvePais>();

            var avesPaises = dbSet.Where(item => item.CodigoAve == codigoAve).AsEnumerable();

            foreach (var item in avesPaises)
            {
                dbSet.Remove(item);
            }
        }
    }
}
