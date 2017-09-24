using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ornithology.Web.Models;
using Ornithology.Services;
using X.PagedList;
using Ornithology.Entity;

namespace Ornithology.Web.Controllers
{
    public class AveController : Controller
    {

        private IAveServices _aveServices = null;
        private IPaisServices _paisServices = null;
        private IZonaServices _zonaServices = null;
        private int pageSize = 5;
        private static List<Pais> paisesDisponibles = new List<Pais>();
        private static List<Zona> zonasDisponibles = new List<Zona>();


        public AveController(IAveServices aveServices, IPaisServices paisServices, IZonaServices zonaServices)
        {
            _aveServices = aveServices;
            _paisServices = paisServices;
            _zonaServices = zonaServices;


        }

        // GET: Ave
        [ValidateInput(false)]
        public async Task<ActionResult> Listar(AveVM ave, int? page = 1)
        {
            zonasDisponibles = await _zonaServices.ListarAsyncAsNoTracking();
            ave.ZonasDisponibles = zonasDisponibles;
            ave.NombreComunOCientifico = ave.NombreComunOCientifico ?? "";
            ave.NombreZona = ave.NombreZona ?? "";
            
            bool hayNombre = !string.IsNullOrEmpty(ave.NombreComunOCientifico);
            bool hayZona = !string.IsNullOrEmpty(ave.NombreZona);
            bool hayFiltros = hayNombre || hayZona;
            

            IEnumerable<AveVM> lista = null;
            List<Ave> aves = new List<Ave>();
            if (!hayFiltros)
            {
                aves = await _aveServices.ListarAsyncAsNoTracking();
                
            }
            else
            {
                ave.NombreComunOCientifico = ave.NombreComunOCientifico.ToUpperInvariant().Trim();
                ave.NombreZona = ave.NombreZona.ToUpperInvariant().Trim();
                aves = await FiltrarResultados(ave, hayNombre, hayZona, aves);

            }
            lista = aves.Select(x => new AveVM
            {
                Codigo = x.Codigo,
                NombreComun = x.NombreComun,
                NombreCientifico = x.NombreCientifico,
                Paises = x.AvesPais.Select(y => y.Pais).ToList()
            });

            IPagedList<AveVM> paginaConAves = lista.ToPagedList(page ?? 1, pageSize);
            ave.Lista = paginaConAves;

            if (Request.IsAjaxRequest()) {
                return PartialView("_AvesPartialView", ave);
            }
            return View(ave);
        }

        private async Task<List<Ave>> FiltrarResultados(AveVM ave, bool hayNombre, bool hayZona, List<Ave> aves)
        {
            if (hayNombre && hayZona)
            {
                aves = await _aveServices
                    .ListarAsyncFilteredAsNoTracking(
                    x => (x.NombreComun.Contains(ave.NombreComunOCientifico) ||
                    x.NombreCientifico.Contains(ave.NombreComunOCientifico)) &&
                    x.AvesPais.Any(avepais => avepais.Pais.Zona.NombreZona == ave.NombreZona), "AvesPais.Pais.Zona");
            }
            else if (hayNombre)
            {
                aves = await _aveServices
                    .ListarAsyncFilteredAsNoTracking(
                    x => x.NombreComun.Contains(ave.NombreComunOCientifico) ||
                    x.NombreCientifico.Contains(ave.NombreComunOCientifico));
            }
            else if (hayZona)
            {
                aves = await _aveServices
                    .ListarAsyncFilteredAsNoTracking(x => x.AvesPais.Any(avepais => avepais.Pais.Zona.NombreZona == ave.NombreZona), "AvesPais.Pais.Zona");
            }

            return aves;
        }

        // GET: Ave/Create
        public async Task<ActionResult> Crear()
        {
            
            paisesDisponibles = await _paisServices.ListarAsyncAsNoTracking();
            

            var aveVM = new AveVM();
            aveVM.PaisesDisponibles = paisesDisponibles;
            aveVM.ZonasDisponibles = zonasDisponibles;

            return View(aveVM);
        }

        // POST: Ave/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear([Bind(Include = "Codigo,NombreComun,NombreCientifico,PaisesSeleccionados")] AveVM aveVM)
        {
            aveVM.PaisesDisponibles = paisesDisponibles;
            aveVM.ZonasDisponibles = zonasDisponibles;

            if (ModelState.IsValid)
            {
                var ave = new Ave { Codigo = aveVM.Codigo.ToUpperInvariant(),
                    NombreComun = aveVM.NombreComun.ToUpperInvariant(),
                    NombreCientifico = aveVM.NombreCientifico.ToUpperInvariant()
                };
                List<AvePais> avePaises = new List<AvePais>();
                foreach (string codigoPais in aveVM.PaisesSeleccionados) {
                    avePaises.Add(new AvePais {
                        CodigoPais = codigoPais,
                        CodigoAve = aveVM.Codigo
                    });
                }
                ave.AvesPais = avePaises;
                RespuestaApi respuesta = await _aveServices.CrearAve(ave);
                bool exito = respuesta.Mensajes.Count == 0;
                if (exito)
                {
                    return RedirectToAction("Listar");
                }
                else {
                    ViewBag.Mensajes = respuesta.Mensajes;
                }
            }

            return View(aveVM);
        }

        //GET: Ave/Edit/5
        public async Task<ActionResult> Editar(string codigo)
        {
            if (codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            paisesDisponibles = await _paisServices.ListarAsyncAsNoTracking();

            Ave ave = await _aveServices.FindAsync(codigo);
            AveVM aveVM = new AveVM {
                Codigo = codigo,
                NombreComun = ave.NombreComun,
                NombreCientifico = ave.NombreCientifico,
                PaisesSeleccionados = ave.AvesPais.Select(x=>x.CodigoPais).ToArray(),
                PaisesDisponibles = paisesDisponibles,
                Paises = ave.AvesPais.Select(y => y.Pais).ToList()
            };
            if (aveVM == null)
            {
                return HttpNotFound();
            }
            return View(aveVM);
        }

        // POST: Ave/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "Codigo,NombreComun,NombreCientifico,Paises,PaisesSeleccionados")] AveVM aveVM)
        {
            aveVM.PaisesDisponibles = paisesDisponibles;
            
            if (ModelState.IsValid)
            {
                Ave ave = await _aveServices.FindAsync(aveVM.Codigo);
                ave.NombreCientifico = aveVM.NombreCientifico.ToUpperInvariant();
                ave.NombreComun = aveVM.NombreComun.ToUpperInvariant();
                
                foreach (string codigoPais in aveVM.PaisesSeleccionados)
                {
                    if (ave.AvesPais.Any(x => x.CodigoPais == codigoPais))
                    {
                        continue;
                    }
                    else {
                        ave.AvesPais.Add(new AvePais
                        {
                            CodigoPais = codigoPais,
                            CodigoAve = aveVM.Codigo
                        });
                    }
                    
                }
                //remover paises no seleccionados
                ave.AvesPais = ave.AvesPais.Where(x=> aveVM.PaisesSeleccionados.Contains(x.CodigoPais)).ToList();


                RespuestaApi respuesta = await _aveServices.ModificarAve(ave);
                bool exito = respuesta.Mensajes.Count == 0;
                if (exito)
                {
                    return RedirectToAction("Listar");
                }
                else {
                    ViewBag.Mensajes = respuesta.Mensajes;
                }
            }
            return View(aveVM);
        }

        //GET: Ave/Delete/5
        public async Task<ActionResult> Eliminar(string codigo)
        {
            if (codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            paisesDisponibles = await _paisServices.ListarAsyncAsNoTracking();

            Ave ave = await _aveServices.FindAsync(codigo);
            AveVM aveVM = new AveVM
            {
                Codigo = codigo,
                NombreComun = ave.NombreComun,
                NombreCientifico = ave.NombreCientifico,
                PaisesSeleccionados = ave.AvesPais.Select(x => x.CodigoPais).ToArray(),
                PaisesDisponibles = paisesDisponibles,
                Paises = ave.AvesPais.Select(y => y.Pais).ToList()
            };
            if (aveVM == null)
            {
                return HttpNotFound();
            }
            return View(aveVM);
        }

        //POST: Ave/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EliminarConfirmado(string codigo)
        {
            
            if (ModelState.IsValid)
            {
                //remover paises
                RespuestaApi respuesta = await _aveServices.EliminarAve(codigo);
                bool exito = respuesta.Mensajes.Count == 0;
                if (exito)
                {
                    return RedirectToAction("Listar");
                }
                else
                {
                    ViewBag.Mensajes = respuesta.Mensajes;
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
