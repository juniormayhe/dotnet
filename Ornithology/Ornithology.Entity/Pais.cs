using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Entity
{
    [Table("TONT_PAISES")]
    public class Pais
    {

        [Key, Column("CDPAIS"), MaxLength(5)]
        public string Codigo { get; set; }

        [Column("DSNOMBRE"), MaxLength(100)]
        public string NombrePais { get; set; }

        [Column("CDZONA"), MaxLength(3)]
        [ForeignKey("Zona")]
        public string CodigoZona { get; set; }
        
        public virtual Zona Zona { get; set; }
        public virtual ICollection<AvePais> AvesPais { get; set; }
    }
}
