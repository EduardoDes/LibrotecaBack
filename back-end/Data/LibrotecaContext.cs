using back_end.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Data
{
    public class LibrotecaContext : DbContext
    {
        public LibrotecaContext(DbContextOptions<LibrotecaContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Libro> Libro { get; set; }
        public DbSet<LibroUsuario> LibroUsuario { get; set; }
        public DbSet<FechaLectura> FechaLectura { get; set; }
        public DbSet<PuntuacionLibro> PuntuacionLibro { get; set; }
        public DbSet<ProgresoLibros> ProgresoLibros { get; set; }
        public DbSet<ProgresoLectura> ProgresoLectura { get; set; }
        public DbSet<Intereses> Intereses { get; set; }
        public DbSet<InteresUsuario> InteresUsuario { get; set; }
    }
}
