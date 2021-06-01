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
    [Route("api/FechaLectura")]
    [ApiController]
    public class FechaLecturaController : ControllerBase
    {
        private readonly LibrotecaContext _contexto;

        public FechaLecturaController(LibrotecaContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FechaLectura>>> GetFechaLectura()
        {
            return await _contexto.FechaLectura.ToListAsync();
        }

        [HttpGet("{idFechaLectura}")]
        public async Task<ActionResult<FechaLectura>> GetFechaLectura(int idFechaLectura)
        {

            var fechaLectura = await _contexto.FechaLectura.FindAsync(idFechaLectura);

            if (fechaLectura == null)
            {
                return NotFound();
            }

            return fechaLectura;
        }

        [HttpPost]
        public async Task<ActionResult<FechaLectura>> PostFechaLectura(FechaLectura fechaLectura)
        {
            _contexto.FechaLectura.Add(fechaLectura);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFechaLectura), new { idFechaLectura = fechaLectura.IdFechaLectura }, fechaLectura);
        }

        [HttpPut("{idFechaLectura}")]
        public async Task<IActionResult> PutFechaLectura(int idFechaLectura, FechaLectura fechaLectura)
        {
            if (idFechaLectura != fechaLectura.IdFechaLectura)
            {
                return BadRequest();
            }

            _contexto.Entry(fechaLectura).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{idFechaLectura}")]

        public async Task<IActionResult> DeleteFechaLectura(int idFechaLectura)
        {
            var fechaLectura = await _contexto.FechaLectura.FindAsync(idFechaLectura);

            if (fechaLectura == null)
            {
                return NotFound();
            }

            _contexto.FechaLectura.Remove(fechaLectura);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
