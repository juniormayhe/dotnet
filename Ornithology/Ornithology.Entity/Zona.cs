using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Entity
{
    [Table("TONT_ZONAS")]
    public class Zona
    {
        public Zona()
        {
            Paises = new HashSet<Pais>();
        }

        [Key, Column("CDZONA"), MaxLength(3)]
        public string Codigo { get; set; }

        [Column("DSNOMBRE"), MaxLength(45)]
        public string NombreZona { get; set; }

        public virtual ICollection<Pais> Paises { get; set; }
    }
}
