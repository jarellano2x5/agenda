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
    public class TelefonoController : ControllerBase
    {
        private readonly dbCtx _ctx;

        public TelefonoController(dbCtx context)
        {
            _ctx = context;
        }

        [HttpGet("{idc}")]
        public IEnumerable<Telefono> Get(int idc)
        {
            List<Telefono> ls = _ctx.Telefonos.Where(t => t.ContactoId == idc).ToList();

            return ls;
        }

        [HttpGet("[action]/{idt}")]
        public async Task<ActionResult<Telefono>> GetById(int idt)
        {
            Telefono tel = await _ctx.Telefonos.FirstOrDefaultAsync(t => t.TelefonoId == idt);
            if (tel == null)
            {
                return NotFound();
            }

            return tel;
        }

        [HttpPost]
        public async Task<ActionResult<Telefono>> Create([FromBody] Telefono tel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _ctx.Telefonos.Add(tel);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { idt = tel.TelefonoId }, tel);
        }

        [HttpPut]
        public async Task<ActionResult<Telefono>> Update([FromBody] Telefono tel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _ctx.Telefonos.Update(tel);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { idt = tel.TelefonoId }, tel);
        }

        [HttpDelete("{idt}")]
        public async Task<ActionResult<bool>> Delete(int idt)
        {
            Telefono tel = await _ctx.Telefonos.FirstOrDefaultAsync(t => t.TelefonoId == idt);
            if (tel == null)
            {
                return NotFound();
            }
            _ctx.Telefonos.Remove(tel);
            await _ctx.SaveChangesAsync();

            return true;
        }
    }
}