﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Models
{
    public class LibroUsuario
    {
        [Key]
        public int IdLibroUsuario { get; set; }
        public int IdUsuario { get; set; }
        public int IdLibro { get; set; }

    }
}
