using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers
{
    [Route("api/LibroUsuario")]
    [ApiController]
    public class LibroUsuarioController : ControllerBase
    {
        private readonly LibrotecaContext _contexto;

        public LibroUsuarioController(LibrotecaContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroUsuario>>> GetLibrosUsuario()
        {
            return await _contexto.LibroUsuario.ToListAsync();
        }

        [HttpGet("{idLibroUsuario}")]
        public async Task<ActionResult<LibroUsuario>> GetLibroUsuario(int idLibroUsuario)
        {

            var libroUsuario = await _contexto.LibroUsuario.FindAsync(idLibroUsuario);

            if (libroUsuario == null)
            {
                return NotFound();
            }

            return libroUsuario;
        }

     

        [HttpGet("GetLibroByUsuario/{IdUsuario}")]
        public async Task<ActionResult<LibroUsuario>> GetLibroByUsuario(int IdUsuario)
        {

            var result = await (from Libro in _contexto.Libro
                                join LibroUsuario in _contexto.LibroUsuario on Libro.IdLibro equals LibroUsuario.IdLibro
                                join Usuario in _contexto.Usuario on LibroUsuario.IdUsuario equals Usuario.IdUsuario
                                join ProgresoLibros in _contexto.ProgresoLibros on LibroUsuario.IdLibro equals ProgresoLibros.IdLibro
                                join PuntuacionLibro in _contexto.PuntuacionLibro on LibroUsuario.IdLibro equals PuntuacionLibro.IdLibro into puntuacion
                                from p in  puntuacion.DefaultIfEmpty()
                                where LibroUsuario.IdUsuario == IdUsuario

                                select new
                                {
                                    NombreLibro = Libro.NombreLibro,
                                    Autor = Libro.AutorLibro,
                                    Imagen = Libro.ImagenLibro,
                                    Genero = Libro.GeneroLibro,
                                    Paginas = Libro.Paginas,
                                    Puntuacion = p.Puntuacion == null ? 0: p.Puntuacion,
                                    ProgresoLibro = ProgresoLibros.PaginasAvance == 0 ? 0 : (ProgresoLibros.PaginasAvance * 100) / Libro.Paginas,
                                    IdLibro = Libro.IdLibro,
                                    PaginasRestantes = Libro.Paginas - ProgresoLibros.PaginasAvance,
                                    IdProgreso = ProgresoLibros.IdProgreso,
                                    PaginasLeidas = ProgresoLibros.PaginasAvance
                                })
                                
                                .OrderByDescending(x => x.ProgresoLibro)
                                .ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetListaLibros/{IdUsuario}")]
        public async Task<ActionResult<LibroUsuario>> GetListaLibros(int IdUsuario)
        {

            var result = await (from Libro in _contexto.Libro
                                where !(from LibroUsuario in _contexto.LibroUsuario where LibroUsuario.IdUsuario == IdUsuario 
                                       select LibroUsuario.IdLibro  ).Contains(Libro.IdLibro)
                                select new
                                {
                                    NombreLibro = Libro.NombreLibro ,
                                    IdLibro = Libro.IdLibro,
                                    Imagen = Libro.ImagenLibro,
                                    Autor = Libro.AutorLibro,
                                    Genero = Libro.GeneroLibro

                                })
                                .OrderByDescending(x => x.NombreLibro)
                                .ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<LibroUsuario>> PostLibroUsuario(LibroUsuario libroUsuario)
        {
            _contexto.LibroUsuario.Add(libroUsuario);
            ProgresoLibros progreLibro = new ProgresoLibros();
            progreLibro.IdUsuario = libroUsuario.IdUsuario;
            progreLibro.IdLibro = libroUsuario.IdLibro;
            progreLibro.PaginasAvance = 0;

            await _contexto.SaveChangesAsync();

            _contexto.ProgresoLibros.Add(progreLibro);

            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLibroUsuario), new { idLibroUsuario = libroUsuario.IdLibroUsuario }, libroUsuario);
        }

        [HttpPut("{idLibroUsuario}")]
        public async Task<IActionResult> PutLibroUsuario(int idLibroUsuario, LibroUsuario libroUsuario)
        {
            if (idLibroUsuario != libroUsuario.IdLibroUsuario)
            {
                return BadRequest();
            }

            _contexto.Entry(libroUsuario).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{idLibroUsuario}")]

        public async Task<IActionResult> DeleteLibroUsuario(int idLibroUsuario)
        {
            var libroUsuario = await _contexto.LibroUsuario.FindAsync(idLibroUsuario);

            if (libroUsuario == null)
            {
                return NotFound();
            }

            _contexto.LibroUsuario.Remove(libroUsuario);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
