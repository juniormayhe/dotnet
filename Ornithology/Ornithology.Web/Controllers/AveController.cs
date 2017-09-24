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
        [HttpPost]
        public ActionResult Listar(int? page = 1)
        {
            AveVM ave = new AveVM();
            return View(ave);
        }
        
        // GET: Ave
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
            else {
                ave.NombreComunOCientifico = ave.NombreComunOCientifico.ToUpperInvariant().Trim();
                ave.NombreZona= ave.NombreZona.ToUpperInvariant().Trim();
                
                aves = await _aveServices
                    .ListarAsyncFilteredAsNoTracking(
                    x=> x.NombreCientifico == ave.NombreComunOCientifico ||
                    x.AvesPais.Any(avepais=>avepais.Pais.Zona.NombreZona == ave.NombreZona), "AvesPais.Pais.Zona");
                
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

        // GET: Ave/Details/5
        //public async Task<ActionResult> Detalles(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AveVM aveVM = await db.AveVMs.FindAsync(id);
        //    if (aveVM == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aveVM);
        //}

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
                var ave = new Ave { Codigo = aveVM.Codigo, NombreComun = aveVM.NombreComun, NombreCientifico = aveVM.NombreCientifico };
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

        // GET: Ave/Edit/5
        //public async Task<ActionResult> Editar(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AveVM aveVM = await db.AveVMs.FindAsync(id);
        //    if (aveVM == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aveVM);
        //}

        // POST: Ave/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Editar([Bind(Include = "Codigo,NombreComun,NombreCientifico")] AveVM aveVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(aveVM).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(aveVM);
        //}

        // GET: Ave/Delete/5
        //public async Task<ActionResult> Eliminar(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AveVM aveVM = await db.AveVMs.FindAsync(id);
        //    if (aveVM == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aveVM);
        //}

        // POST: Ave/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> EliminarConfirmado(string id)
        //{
        //    AveVM aveVM = await db.AveVMs.FindAsync(id);
        //    db.AveVMs.Remove(aveVM);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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
