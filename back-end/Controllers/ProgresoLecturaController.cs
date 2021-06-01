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
    [Route("api/ProgresoLectura")]
    [ApiController]
    public class ProgresoLecturaController : ControllerBase
    {
        private readonly LibrotecaContext _contexto;

        public ProgresoLecturaController(LibrotecaContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgresoLectura>>> GetProgresoLectura()
        {
            return await _contexto.ProgresoLectura.ToListAsync();
        }

        [HttpGet("{idProgresoLectura}")]
        public async Task<ActionResult<ProgresoLectura>> GetProgresoLectura(int idProgresoLectura)
        {

            var progresoLectura = await _contexto.ProgresoLectura.FindAsync(idProgresoLectura);

            if (progresoLectura == null)
            {
                return NotFound();
            }

            return progresoLectura;
        }

        [HttpPost]
        public async Task<ActionResult<ProgresoLectura>> PostProgresoLectura(ProgresoLectura progresoLectura)
        {
            _contexto.ProgresoLectura.Add(progresoLectura);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProgresoLectura), new { idProgresoLectura = progresoLectura.IdProgresoLectura }, progresoLectura);
        }

        [HttpPut("{idProgresoLectura}")]
        public async Task<IActionResult> PutProgresoLectura(int idProgresoLectura, ProgresoLectura progresoLectura)
        {
            if (idProgresoLectura != progresoLectura.IdProgresoLectura)
            {
                return BadRequest();
            }

            _contexto.Entry(progresoLectura).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{idProgresoLectura}")]

        public async Task<IActionResult> DeleteProgresoLectura(int idProgresoLectura)
        {
            var progresoLectura = await _contexto.ProgresoLectura.FindAsync(idProgresoLectura);

            if (progresoLectura == null)
            {
                return NotFound();
            }

            _contexto.ProgresoLectura.Remove(progresoLectura);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
