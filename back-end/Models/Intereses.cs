using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Models
{
    public class Intereses
    {
        [Key]
        public int IdInteres { get; set; }
        public string Descripcion { get; set; }
    }
}
