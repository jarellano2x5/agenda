using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using agenda.Data;
using agenda.Models;

namespace agenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly dbCtx _ctx;

        public UsuarioController(dbCtx context)
        {
            _ctx = context;
        }

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            List<Usuario> ls = _ctx.Usuarios.ToList();

            return ls;
        }

        [HttpGet("{idu}")]
        public async Task<ActionResult<Usuario>> Get(int idu)
        {
            Usuario usu = await _ctx.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId == idu);
            if (usu == null)
            {
                return NotFound();
            }

            return usu;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create([FromBody] Usuario usu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _ctx.Usuarios.Add(usu);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { idu = usu.UsuarioId }, usu);
        }

        [HttpPut]
        public async Task<ActionResult<Usuario>> Update([FromBody] Usuario usu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _ctx.Usuarios.Update(usu);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { idu = usu.UsuarioId }, usu);
        }
    }
}