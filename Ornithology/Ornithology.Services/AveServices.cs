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
    public class AveServices : IAveServices
    {
        private IAveRepository _aveRepository;
        private IAveDomain _aveDomain;

        public AveServices(IAveRepository aveRepository, IAveDomain aveDomain)
        {
            _aveRepository = aveRepository;
            _aveDomain = aveDomain;
        }

        public async Task<Ave> FindAsync(string codigoAve)
        {
            return await _aveRepository.GetFirstAsync(x=>x.Codigo == codigoAve);
        }

        public async Task<List<Ave>> ListarAsync()
        {
            return await _aveRepository.GetAllAsync();
        }
        
        public async Task<List<Ave>> ListarAsyncFilteredAsNoTracking(Expression<Func<Ave, bool>> filter, params string[] include)
        {
            return await _aveRepository.GetFilteredAsyncAsNoTracking(filter, include);
        }

        public async Task<List<Ave>> ListarAsyncAsNoTracking()
        {
            return await _aveRepository.GetFilteredAsyncAsNoTracking();
        }
        
        public async Task<RespuestaApi> CrearAve(Ave ave)
        {
            var respuesta = new RespuestaApi();

            var mensajes = new List<string>();
            
            bool aveValida = _aveDomain.Validar(ave, ref mensajes);
            Ave aveExistente = await ValidarAveExistente(ave, mensajes);
            
            
            if (aveValida && mensajes.Count == 0)
            {
                if (aveExistente == null)
                {
                    _aveRepository.Add(ave);
                }
                else
                {
                    foreach (AvePais avepais in ave.AvesPais)
                    {
                        aveExistente.AvesPais.Add(avepais);
                    }
                    _aveRepository.Modify(aveExistente);

                }
                int resultado = await _aveRepository.UnitOfWork.CommitAsync();

            }
            else
            {
                respuesta = new RespuestaApi { Mensajes = mensajes };
            }

            respuesta.Mensajes = mensajes;
            return respuesta;
        }

        private async Task<Ave> ValidarAveExistente(Ave ave, List<string> mensajes)
        {
            var aveExistente = await _aveRepository.GetFirstAsync(item => item.Codigo == ave.Codigo);
            if (aveExistente != null)
            {
                bool aveYaEstaEnPais = aveExistente.AvesPais.Select(x => x.CodigoPais)
                                          .Intersect(ave.AvesPais.Select(y => y.CodigoPais))
                                          .Any();
                if (aveYaEstaEnPais)
                {
                    mensajes.Add($"Ya existe una ave con el código {aveExistente.Codigo} en el país seleccionado");
                }
            }

            return aveExistente;
        }

        public async Task<RespuestaApi> ModificarAve(Ave ave)
        {
            var respuesta = new RespuestaApi();

            var mensajes = new List<string>();

            bool aveValida = _aveDomain.Validar(ave, ref mensajes);

            if (aveValida && mensajes.Count == 0)
            {
                //foreach (AvePais avepais in ave.AvesPais)
                //{
                //    ave.AvesPais.Add(avepais);
                //}
                
                _aveRepository.Modify(ave);

                
                int resultado = await _aveRepository.UnitOfWork.CommitAsync();
            }
            else
            {
                respuesta = new RespuestaApi { Mensajes = mensajes };
            }

            respuesta.Mensajes = mensajes;
            return respuesta;
        }

        public async Task<RespuestaApi> EliminarAve(string codigoAve)
        {
            var respuesta = new RespuestaApi();

            var mensajes = new List<string>();

            Ave ave = await _aveRepository
                .GetFirstAsync(item => item.Codigo == codigoAve);

            bool aveValida = ave != null;

            if (aveValida)
            {
                _aveRepository.DeleteCascade(ave.Codigo);
                int resultado = await _aveRepository.UnitOfWork.CommitAsync();
            }
            else
            {
                respuesta = new RespuestaApi { Mensajes = mensajes };
            }

            respuesta.Mensajes = mensajes;
            
            return respuesta;
        }

        
    }
}
