using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Entity
{
    [Table("TONT_AVES")]
    public class Ave
    {
        public Ave()
        {
            AvesPais = new HashSet<AvePais>();
        }
        [Key, Column("CDAVE"), MaxLength(5)]
        public string Codigo { get; set; }

        [Column("DSNOMBRE_COMUN"), MaxLength(100)]
        public string NombreComun { get; set; }

        [Column("DSNOMBRE_CIENTIFICO"), MaxLength(200)]
        public string NombreCientifico { get; set; }

        public virtual ICollection<AvePais> AvesPais { get; set; }

        
    }
}
