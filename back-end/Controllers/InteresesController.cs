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
    [Route("api/Intereses")]
    public class InteresesController : ControllerBase
    {
        private readonly LibrotecaContext _contexto;

        public InteresesController(LibrotecaContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intereses>>> GetIntereses()
        {
            return await _contexto.Intereses.ToListAsync();
        }

    }
}
