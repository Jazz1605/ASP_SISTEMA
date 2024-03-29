﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistemas.Datos;
using Sistemas.Entidades.Usuarios;
using Sistemas.Web.Models.Usuarios.Usuario;

namespace Sistemas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DBContextSistema _context;

        public UsuariosController(DBContextSistema context)
        {
            _context = context;
        }

       // GET: api/Usuarios/Listar
       [HttpGet("[action]")]
       public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            var usuario = await _context.Usuarios.Include(u => u.rol).ToListAsync();
            return usuario.Select(u => new UsuarioViewModel
            {
                idusuario = u.idusuario,
                idrol = u.idrol,
                rol = u.rol.nombre,
                nombre = u.nombre,
                tipo_documento = u.tipo_documento,
                num_documento = u.num_documento,
                direccion = u.direccion,
                telefono = u.telefono,
                email = u.email,
                password_hash = u.password_hash,
                condicion = u.condicion
            });
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        // POST: api/Usuarios/CrearUsuario
        [HttpPost("[action]")]
        public async Task<IActionResult> CrearUsuario([FromBody] CrearUsuario model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
            var email = model.email.ToLower();
            if (await _context.Usuarios.AnyAsync(u => u.email == email))
            {
                return BadRequest("El email ya existe");
            }
            Usuario usuario = new Usuario
            {

                idrol = model.idrol,
                nombre = model.nombre,
                tipo_documento = model.tipo_documento,
                num_documento = model.num_documento,
                direccion = model.direccion,
                telefono = model.telefono,
                email = model.email,
                password_hash = passwordHash,
                password_salt = passwordSalt,
                condicion = true
            };

           
            
            _context.Usuarios.Add(usuario);
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
        // PUT: api/Usuarios/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarUsuario model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idusuario <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(c => c.idusuario == model.idusuario);
            if (usuario == null)
            {
                return NotFound(); //pagina error
            }

            usuario.idrol = model.idrol;
            usuario.nombre = model.nombre;
            usuario.tipo_documento = model.tipo_documento;
            usuario.num_documento = model.num_documento;
            usuario.direccion = model.direccion;
            usuario.telefono = model.telefono;
            usuario.email = model.email.ToLower();

            if (model.act_password == true)
            {
                CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.password_hash = passwordHash;
                usuario.password_salt = passwordSalt;
            }

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





        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.idusuario == id);
        }
    }
}