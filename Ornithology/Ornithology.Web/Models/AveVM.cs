using Ornithology.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using X.PagedList;

namespace Ornithology.Web.Models
{
    public class AveVM
    {
        public AveVM()
        {
            NombreComunOCientifico = string.Empty;
            NombreZona= string.Empty;
        }
        [Key, Required(ErrorMessage = "Por favor ingrese el Código del ave")]
        [Display(Name = "Código")]
        [MinLength(1, ErrorMessage = "El Código debe conter en lo mínimo 1 caracteres"), MaxLength(5, ErrorMessage = "El Código debe contener en lo máximo 5 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el Nombre común del ave")]
        [Display(Name = "Nombre común")]
        [MinLength(5, ErrorMessage = "El Nombre común debe conter en lo mínimo 5 caracteres"), MaxLength(100, ErrorMessage = "El Nombre común debe contener en lo máximo 100 caracteres")]
        public string NombreComun { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el Nombre científico del ave")]
        [Display(Name = "Nombre científico")]
        [MinLength(5, ErrorMessage = "El Nombre científico debe conter en lo mínimo 5 caracteres"), MaxLength(200, ErrorMessage = "El Nombre común debe contener en lo máximo 200 caracteres")]
        public string NombreCientifico { get; set; }
        
        [Required(ErrorMessage = "Por favor seleccione un País para el ave")]
        public string[] PaisesSeleccionados { set; get; }

        [Display(Name = "Países")]
        public List<Pais> PaisesDisponibles { get; internal set; }
        public List<Zona> ZonasDisponibles { get; internal set; }

        //lista de aves
        public IPagedList<AveVM> Lista { get; set; }

        public List<Pais> Paises { get; set; }

        
        public string PaisesComoTexto => Paises.Count() ==0 ? "Sin país definido" : string.Join(",", 
            Paises.Select(x => x.NombrePais + " " + (x.Zona == null ? "(Sin zona)" : "(Zona " + x.Zona?.NombreZona + ")")));
        

        //[Display(Name = "Nombre común o científico")]
        //[MinLength(5, ErrorMessage = "El Nombre común debe contener en lo mínimo 5 caracteres"), MaxLength(100, ErrorMessage = "El Nombre común debe contener en lo máximo 100 caracteres")]
        public string NombreComunOCientifico { get; set; }


        //[Display(Name = "Zona científico")]
        //[MinLength(5, ErrorMessage = "La Zona debe contener en lo mínimo 5 caracteres"), MaxLength(45, ErrorMessage = "La Zona debe contener en lo máximo 45 caracteres")]
        public string NombreZona { get; set; }
        
    }
}