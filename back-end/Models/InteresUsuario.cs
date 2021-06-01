using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Models
{
    public class InteresUsuario
    {
        [Key]
        public int IdInteresUsuario { get; set; }
        public int Interes1 { get; set; }
        public int Interes2 { get; set; }
        public int Interes3 { get; set; }
        public int IDUsuario { get; set; }
    }
}
