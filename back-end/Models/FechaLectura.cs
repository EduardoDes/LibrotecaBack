using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Models
{
    public class FechaLectura
    {
        [Key]
        public int IdFechaLectura { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaDeLectura { get; set; }
        public int CantidadLeidas { get; set; }
    }
}
