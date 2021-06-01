using back_end.Data;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Controllers
{
    [Route("api/Usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly LibrotecaContext _contexto;

        public UsuarioController(LibrotecaContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarioItems()
        {
            return await _contexto.Usuario.ToListAsync();
        }

        [HttpGet]
        [Route("getUltimosRegistrados")]
        public async Task<ActionResult<Usuario>> getUltimosRegistrados()
        {
            var result = await (from Usuario in _contexto.Usuario
                                join InteresUsuario in _contexto.InteresUsuario on Usuario.IdUsuario equals InteresUsuario.IDUsuario
                     
                                select new
                                {
                                    NombreUsuario = Usuario.NombreUsuario,
                                    Presentacion = Usuario.Presentacion,
                                    IdUsuario = Usuario.IdUsuario,
                                    Interes1 = InteresUsuario.Interes1,
                                    Interes2 = InteresUsuario.Interes2,
                                    Interes3 = InteresUsuario.Interes3,
                                    Imagen = Usuario.Imagen
                                })
                                .OrderByDescending(x => x.IdUsuario)
                                .Take(4)
                                .ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }



        [HttpGet]
        [Route("getTopUsuarios")]
        public async Task<ActionResult<Usuario>> getTopUsuarios()
        {
            var result = await (from Usuario in _contexto.Usuario
                                join FechaLectura in _contexto.FechaLectura on Usuario.IdUsuario equals FechaLectura.IdUsuario
                                group FechaLectura by new { FechaLectura.IdUsuario , Usuario.NombreUsuario, Usuario.Imagen }
                                into response
                                select new
                                {
                                    IdUsuario = response.Key.IdUsuario,
                                    NombreUsuario = response.Key.NombreUsuario,
                                    Imagen = response.Key.Imagen,
                                    TotalPaginas = response.Sum(x => x.CantidadLeidas)
                                })
                                .OrderByDescending(x => x.TotalPaginas)
                                .Take(4)
                                .ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpGet]
        [Route("getEstadisticasUsuarios")]
        public async Task<ActionResult<Usuario>> getEstadisticasUsuarios()
        {
            var result = await (from FechaLectura in _contexto.FechaLectura
                             
                                select new
                                {
                                   
                                    TotalPaginas = FechaLectura.CantidadLeidas
                                })
                                .ToListAsync();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpGet("{idUsuario}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int idUsuario)
        {

            var usuario = await _contexto.Usuario.FindAsync(idUsuario);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _contexto.Usuario.Add(usuario);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { idUsuario = usuario.IdUsuario }, usuario);
        }

        [HttpPut("{idUsuario}")]
        public async Task<IActionResult> PutUsuario(int idUsuario, Usuario usuario)
        {
            if(idUsuario != usuario.IdUsuario)
            {
                return BadRequest();
            }

            _contexto.Entry(usuario).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{idUsuario}")]

        public async Task<IActionResult> DeleteUsuario(int idUsuario)
        {
            var usuario = await _contexto.Usuario.FindAsync(idUsuario);

            if(usuario == null)
            {
                return NotFound();
            }

            _contexto.Usuario.Remove(usuario);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
            
    }
}
