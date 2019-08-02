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
    public class ContactoController : ControllerBase
    {
        private readonly dbCtx _ctx;

        public ContactoController(dbCtx context)
        {
            _ctx = context;
        }

        [HttpGet("{idu}")]
        public IEnumerable<Contacto> Get(int idu)
        {
            List<Contacto> ls = _ctx.Contactos.Where(c => c.UsuarioId == idu).ToList();

            return ls;
        }

        [HttpGet("[action]/{idu}")]
        public async Task<ActionResult<Contacto>> GetById(int idc)
        {
            Contacto con = await _ctx.Contactos.FirstOrDefaultAsync(c => c.ContactoId == idc);
            if (con == null)
            {
                return NotFound();
            }

            return con;
        }

        [HttpPost]
        public async Task<ActionResult<Contacto>> Create([FromBody] Contacto con)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _ctx.Contactos.Add(con);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { idc = con.ContactoId }, con);
        }

        [HttpPut]
        public async Task<ActionResult<Contacto>> Update([FromBody] Contacto con)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _ctx.Contactos.Update(con);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { idc = con.ContactoId }, con);
        }

        [HttpDelete("{idc}")]
        public async Task<ActionResult<bool>> Delete(int idc)
        {
            Contacto con = await _ctx.Contactos.FirstOrDefaultAsync(c => c.ContactoId == idc);
            if (con == null)
            {
                return NotFound();
            }
            List<Telefono> tels = await _ctx.Telefonos.Where(t => t.ContactoId == con.ContactoId).ToListAsync();
            if (tels.Any())
            {
                _ctx.Telefonos.RemoveRange(tels);
                await _ctx.SaveChangesAsync();
            }
            _ctx.Contactos.Remove(con);
            await _ctx.SaveChangesAsync();

            return true;
        }
    }
}