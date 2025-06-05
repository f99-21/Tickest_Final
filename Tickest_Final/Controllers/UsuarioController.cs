
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Tickest_Final.Models;

namespace GestionTickets.Controllers
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

            if (usuario == null || !VerificarContrasena(model.contrasena, usuario.contrasena))
            {
                ViewBag.ErrorMessage = "Credenciales inválidas";
                return View();
            }

            // Redirigir al dashboard o alguna vista principal
            return RedirectToAction("Dashboard", "Home");
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
                string contrasenaTemporal = model.contrasena;
                model.contrasena = EncriptarContrasena(model.contrasena);

                _ticketsContexto.usuario.Add(model);
                await _ticketsContexto.SaveChangesAsync();

                // Enviar correo con la contraseña temporal
                // Puedes usar una función similar a la que ya tienes para enviar el correo

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

                // Verificar contraseña actual
                if (!VerificarContrasena(model.ContrasenaActual, usuario.contrasena))
                {
                    ViewBag.Message = "La contraseña actual es incorrecta";
                    return View();
                }

                // Validar que la nueva contraseña no sea igual a la anterior
                if (VerificarContrasena(model.NuevaContrasena, usuario.contrasena))
                {
                    ViewBag.Message = "La nueva contraseña no puede ser igual a la actual";
                    return View();
                }

                // Hashear y guardar la nueva contraseña
                usuario.contrasena = EncriptarContrasena(model.NuevaContrasena);
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

        // Método para encriptar la contraseña
        public static string EncriptarContrasena(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        public static bool VerificarContrasena(string enteredPassword, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
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