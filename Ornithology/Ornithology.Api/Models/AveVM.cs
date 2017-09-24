using Ornithology.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using X.PagedList;

namespace Ornithology.WebApi.Models
{
    public class AveVM
    {
        [Key]
        [Display(Name = "Código")]
        [MinLength(1, ErrorMessage = "El código deve contener al menos 1 caracter"), MaxLength(5, ErrorMessage = "El código del ave no puede superar 5 caracteres")]
        public string CodigoAve { get; set; }

        [Display(Name = "Nombre común")]
        [Required(ErrorMessage = "Por favor informe el nombre común")]
        [MinLength(1, ErrorMessage = "El nombre común deve contener al menos 2 caracteres"), MaxLength(100, ErrorMessage = "El nombre común del ave no puede superar 100 caracteres")]
        public string QuestaoEnunciado { get; set; }

        [Display(Name = "Nombre científico")]
        [Required(ErrorMessage = "Por favor informe el nombre científico")]
        [MinLength(1, ErrorMessage = "El nombre científico deve contener al menos 2 caracteres"), MaxLength(100, ErrorMessage = "El nombre científico del ave no puede superar 100 caracteres")]
        
        public virtual ICollection<AvePais> AvesPaises { get; set; }
        
        public IPagedList<Ave> Lista { get; set; }
    }
}