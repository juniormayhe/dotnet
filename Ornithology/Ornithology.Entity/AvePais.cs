using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Entity
{
    [Table("TONT_AVES_PAIS")]
    public class AvePais
    {
        [Key, Column("CDPAIS", Order = 0), MaxLength(3)]
        [ForeignKey("Pais")]
        public string CodigoPais { get; set; }

        [Key, Column("CDAVE", Order = 1), MaxLength(5)]
        [ForeignKey("Ave")]
        public string CodigoAve { get; set; }
        

        public virtual Ave Ave {get;set;}
        public virtual Pais Pais { get; set; }
    }
}
