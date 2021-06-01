using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Models
{
    public class Libro
    {
        [Key]
        public int IdLibro { get; set; }
        public string NombreLibro { get; set; }
        public string AutorLibro { get; set; }
        public int AcnoLibro { get; set; }
        public string ImagenLibro { get; set; }
        public string GeneroLibro { get; set; }
        public string Resegna { get; set; }
        public int Paginas { get; set; }
    }
}
