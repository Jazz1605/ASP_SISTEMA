using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistemas.Datos;
using Sistemas.Entidades.Usuarios;
using Sistemas.Web.Models.Almacen.Rol;

namespace Sistemas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolsController : ControllerBase
    {
        private readonly DBContextSistema _context;

        public RolsController(DBContextSistema context)
        {
            _context = context;
        }

        // GET: api/Rols/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolViewModel>> Listar()
        {
            var rol = await _context.Roles.ToListAsync();

            return rol.Select(r => new RolViewModel
            {
                idrol = r.idrol,
                nombre = r.nombre,
                descripcion = r.descripcion,
                condicion = r.condicion
            });
        }

        // GET: api/Rols/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }
            return Ok(new RolViewModel
            {

                idrol = rol.idrol,
                nombre = rol.nombre,
                descripcion = rol.descripcion,
                condicion = rol.condicion

            });


        }

        // PUT: api/Rols/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idrol <= 0)
            {
                return BadRequest();
            }

            var rol = await _context.Roles.FirstOrDefaultAsync(c => c.idrol == model.idrol);
            if (rol == null)
            {
                return NotFound(); //pagina error
            }

            rol.nombre = model.nombre;
            rol.descripcion = model.descripcion;



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //guardar la excepcio 
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Rols/CrearRol
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearRol([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Rol rol = new Rol
            {

                nombre = model.nombre,
                descripcion = model.descripcion,
                condicion = true

            };
            _context.Roles.Add(rol);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Rols/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteRol([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(rol);
            try
            {

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            await _context.SaveChangesAsync();

            return Ok(rol);
        }

        private bool RolesExists(int id)
        {
            return _context.Roles.Any(e => e.idrol == id);
        }
    }
}