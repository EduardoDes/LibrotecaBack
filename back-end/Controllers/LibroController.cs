using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/Libro")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly LibrotecaContext _contexto;

        public LibroController(LibrotecaContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            return await _contexto.Libro.ToListAsync();
        }

        [HttpGet("{idLibro}")]
        public async Task<ActionResult<Libro>> GetLibro(int idLibro)
        {

            var libro = await _contexto.Libro.FindAsync(idLibro);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        [HttpGet]
        [Route("getUltimosComentarios")]
        public async Task<ActionResult<Libro>> getUltimosComentarios()
        {
            var result = await (from Puntuacion in _contexto.PuntuacionLibro
                                join Libro in _contexto.Libro on Puntuacion.IdLibro equals Libro.IdLibro
                                join Usuario in _contexto.Usuario on Puntuacion.IdUsuario equals Usuario.IdUsuario

                                select new
                                {
                                    NombreLibro = Libro.NombreLibro,
                                    Puntuacion = Puntuacion.Puntuacion,
                                    Imagen = Libro.ImagenLibro,
                                    NombreUsuario = Usuario.NombreUsuario,
                                    IdPuntuacion = Puntuacion.IdPuntuacion,
                                    Comentario = Puntuacion.ComentarioLibro,
                                    IdLibro = Libro.IdLibro

                                })
                                .OrderByDescending(x => x.IdPuntuacion)
                                .Take(3)
                                .ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetComentarioByIdLibro/{IdLibro}")]
        public async Task<ActionResult<Libro>> GetComentarioByIdLibro(int IdLibro)
        {

            var result = await (from Puntuacion in _contexto.PuntuacionLibro
                                join Libro in _contexto.Libro on Puntuacion.IdLibro equals Libro.IdLibro
                                join Usuario in _contexto.Usuario on Puntuacion.IdUsuario equals Usuario.IdUsuario
                                where Libro.IdLibro == IdLibro

                                select new
                                {
                                    NombreLibro = Libro.NombreLibro,
                                    Puntuacion = Puntuacion.Puntuacion,
                                    Imagen = Usuario.Imagen,
                                    NombreUsuario = Usuario.NombreUsuario,
                                    IdPuntuacion = Puntuacion.IdPuntuacion,
                                    Comentario = Puntuacion.ComentarioLibro,
                                    IdLibro = Libro.IdLibro

                                })
                                 .OrderByDescending(x => x.IdPuntuacion)
                                 .ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpGet]
        [Route("getUltimosAgregados")]
        public async Task<ActionResult<Libro>> getUltimosAgregados()
        {
            var result = await (from Libro in _contexto.Libro
                               

                                select new
                                {
                                    NombreLibro = Libro.NombreLibro,
                                    Imagen = Libro.ImagenLibro,
                                    IdLibro = Libro.IdLibro,
                                    AutorLibro = Libro.AutorLibro,
                                    Resegna = Libro.Resegna,
                                    Paginas = Libro.Paginas

                                })
                                .OrderByDescending(x => x.IdLibro)
                                .Take(3)
                                .ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Libro>> PostLibro(Libro libro)
        {
            _contexto.Libro.Add(libro);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLibro), new { idLibro = libro.IdLibro }, libro);
        }

        [HttpPut("{idLibro}")]
        public async Task<IActionResult> PutLibro(int idLibro, Libro libro)
        {
            if (idLibro != libro.IdLibro)
            {
                return BadRequest();
            }

            _contexto.Entry(libro).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{idLibro}")]

        public async Task<IActionResult> DeleteLibro(int idLibro)
        {
            var libro = await _contexto.Libro.FindAsync(idLibro);

            if (libro == null)
            {
                return NotFound();
            }

            _contexto.Libro.Remove(libro);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
