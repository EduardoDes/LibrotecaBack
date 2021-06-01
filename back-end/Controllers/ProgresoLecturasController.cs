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
    [Route("api/ProgresoLecturas")]
    [ApiController]
    public class ProgresoLecturasController : ControllerBase
    {
        private readonly LibrotecaContext _contexto;

        public ProgresoLecturasController(LibrotecaContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgresoLibros>>> GetProgresoLibros()
        {
            return await _contexto.ProgresoLibros.ToListAsync();
        }

        [HttpGet("{idProgreso}")]
        public async Task<ActionResult<ProgresoLibros>> GetProgresoLibro(int idProgreso)
        {

            var progresoLibros = await _contexto.ProgresoLibros.FindAsync(idProgreso);

            if (progresoLibros == null)
            {
                return NotFound();
            }

            return progresoLibros;
        }

        [HttpPost]
        public async Task<ActionResult<ProgresoLibros>> PostProgresoLibros(ProgresoLibros progresoLibros)
        {
            _contexto.ProgresoLibros.Add(progresoLibros);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProgresoLibros), new { idProgreso = progresoLibros.IdProgreso }, progresoLibros);
        }

        [HttpPut("{idProgreso}")]
        public async Task<IActionResult> PutProgresoLibros(int idProgreso, ProgresoLibros progresoLibros)
        {
            if (idProgreso != progresoLibros.IdProgreso)
            {
                return BadRequest();
            }

            _contexto.Entry(progresoLibros).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{idProgreso}")]

        public async Task<IActionResult> DeleteProgresoLibros(int idProgreso)
        {
            var progresoLibros = await _contexto.ProgresoLibros.FindAsync(idProgreso);

            if (progresoLibros == null)
            {
                return NotFound();
            }

            _contexto.ProgresoLibros.Remove(progresoLibros);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
