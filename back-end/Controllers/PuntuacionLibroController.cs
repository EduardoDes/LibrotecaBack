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
    [Route("api/PuntuacionLibro")]
    [ApiController]
    public class PuntuacionLibroController : ControllerBase
    {
        private readonly LibrotecaContext _contexto;

        public PuntuacionLibroController(LibrotecaContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PuntuacionLibro>>> GetPuntuacionLibros()
        {
            return await _contexto.PuntuacionLibro.ToListAsync();
        }

        [HttpGet]
        [Route("getLibrosRankeados")]
        public async Task<ActionResult<PuntuacionLibro>>  GetLibrosRankeados()
        {
            var result = await (from Puntuacion in _contexto.PuntuacionLibro
                                join Libro in _contexto.Libro on Puntuacion.IdLibro equals Libro.IdLibro

                                select new
                                {
                                    NombreLibro = Libro.NombreLibro,
                                    Autor = Libro.AutorLibro,
                                    Puntuacion = Puntuacion.Puntuacion,
                                    Imagen = Libro.ImagenLibro,
                                    IdLibro = Libro.IdLibro
                                })
                                .OrderByDescending(x => x.Puntuacion)
                                .Take(3)
                                .ToListAsync();
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{idPuntuacion}")]
        public async Task<ActionResult<PuntuacionLibro>> GetPuntuacionLibro(int idPuntuacion)
        {

            var puntuacionLibro = await _contexto.PuntuacionLibro.FindAsync(idPuntuacion);

            if (puntuacionLibro == null)
            {
                return NotFound();
            }

            return puntuacionLibro;
        }

        [HttpPost]
        public async Task<ActionResult<PuntuacionLibro>> PostPuntuacionLibro(PuntuacionLibro puntuacionLibro)
        {
            _contexto.PuntuacionLibro.Add(puntuacionLibro);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPuntuacionLibro), new { idPuntuacion = puntuacionLibro.IdPuntuacion }, puntuacionLibro);
        }

        [HttpPut("{idPuntuacion}")]
        public async Task<IActionResult> PutPuntuacionLibro(int idPuntuacion, PuntuacionLibro puntuacionLibro)
        {
            if (idPuntuacion != puntuacionLibro.IdPuntuacion)
            {
                return BadRequest();
            }

            _contexto.Entry(puntuacionLibro).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{idPuntuacion}")]

        public async Task<IActionResult> DeletePuntuacionLibro(int idPuntuacion)
        {
            var puntacionLibro = await _contexto.PuntuacionLibro.FindAsync(idPuntuacion);

            if (puntacionLibro == null)
            {
                return NotFound();
            }

            _contexto.PuntuacionLibro.Remove(puntacionLibro);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
