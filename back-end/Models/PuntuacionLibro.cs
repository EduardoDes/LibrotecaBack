using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Models
{
    public class PuntuacionLibro
    {
        [Key]
        public int IdPuntuacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdLibro { get; set; }
        public int Puntuacion { get; set; }
        public string ComentarioLibro { get; set; }
    }
}
