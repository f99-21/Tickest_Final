using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tickest_Final.Models;

namespace Tickest_Final.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ticketsContext _ticketsContexto;

        public UsuarioController(ticketsContext ticketsContexto)
        {
            _ticketsContexto = ticketsContexto;
        }

        // Vista de Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Acción para el login
        [HttpPost]
        public async Task<IActionResult> Login(login model)
        {
            var usuario = await _ticketsContexto.usuario.FirstOrDefaultAsync(u => u.correo == model.correo);

            if (usuario == null || usuario.contrasena != model.contrasena)
            {
                ViewBag.ErrorMessage = "Credenciales inválidas";
                return View();
            }

            // Redirigir al dashboard o alguna vista principal
            return RedirectToAction("Dashboard", "Usuario");
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }


        // Vista de registro de usuario externo
        [HttpGet]
        public IActionResult RegistrarExterno()
        {
            return View();
        }

        // Acción para registrar un usuario externo
        [HttpPost]
        public async Task<IActionResult> RegistrarExterno(usuario model)
        {
            try
            {
                bool correoExiste = await _ticketsContexto.usuario.AnyAsync(u => u.correo == model.correo);
                if (correoExiste)
                {
                    ViewBag.Message = "El correo ya está registrado.";
                    return View(model);
                }

                model.tipo_usuario = "externo";
                model.rol = "cliente";

                _ticketsContexto.usuario.Add(model);
                await _ticketsContexto.SaveChangesAsync();

                ViewBag.Message = "Usuario externo registrado exitosamente.";
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error al registrar el usuario: " + ex.Message;
                return View(model);
            }
        }

        // Vista de cambio de contraseña
        [HttpGet]
        public IActionResult CambiarContrasena()
        {
            return View();
        }

        // Acción para cambiar la contraseña
        [HttpPost]
        public async Task<IActionResult> CambiarContrasena(CambioContrasenaModel model)
        {
            try
            {
                var usuario = await _ticketsContexto.usuario.FindAsync(model.IdUsuario);
                if (usuario == null)
                {
                    ViewBag.Message = "Usuario no encontrado";
                    return View();
                }

                if (usuario.contrasena != model.ContrasenaActual)
                {
                    ViewBag.Message = "La contraseña actual es incorrecta";
                    return View();
                }

                if (usuario.contrasena == model.NuevaContrasena)
                {
                    ViewBag.Message = "La nueva contraseña no puede ser igual a la actual";
                    return View();
                }

                usuario.contrasena = model.NuevaContrasena;
                _ticketsContexto.usuario.Update(usuario);
                await _ticketsContexto.SaveChangesAsync();

                ViewBag.Message = "Contraseña cambiada exitosamente";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error al cambiar la contraseña: " + ex.Message;
                return View();
            }
        }
    }

    // Modelo para el cambio de contraseña
    public class CambioContrasenaModel
    {
        public int IdUsuario { get; set; }
        public string ContrasenaActual { get; set; }
        public string NuevaContrasena { get; set; }
    }
}
