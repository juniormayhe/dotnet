using Ornithology.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Services
{
    public interface IAveServices : IService<Ave>, IServiceAsNoTracking<Ave> {

        Task<RespuestaApi> CrearAve(Ave ave);

        Task<RespuestaApi> ModificarAve(Ave ave);

        Task<RespuestaApi> EliminarAve(string codigoAve);
        
    }
}
