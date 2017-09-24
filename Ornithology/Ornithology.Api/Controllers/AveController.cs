using Ornithology.Entity;
using Ornithology.Services;
using Ornithology.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using X.PagedList;

namespace Ornithology.WebApi.Controllers
{
    public class AveController : ApiController
    {
        private IAveServices _aveServices = null;
        private const int pageSize = 5;

        public AveController(IAveServices aveServices)
        {
            _aveServices = aveServices;
        }

        public async Task<ActionResult> Listar(int? page = 1)
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://yourapi.com/api/ave");

            var lista2 = await _aveServices.ListarAsync();

            var lista = await response.Content.ReadAsAsync<IEnumerable<Ave>>();
            IPagedList<Ave> paginasConAves = lista.ToPagedList(page ?? 1, pageSize);
            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_QuestoesPartialView", vm);
            //}
            return View(paginasConAves);
        }

        // GET: api/Ave
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Ave/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Ave
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Ave/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Ave/5
        public void Delete(int id)
        {
        }
    }
}
