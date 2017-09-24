using Ornithology.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Domain
{
    public interface IDomain<TEntity> where TEntity : class
    {
        bool Validar(TEntity objeto, ref List<string> mensajes);
    }
}
