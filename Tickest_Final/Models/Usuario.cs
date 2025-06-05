using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public string Empresa { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [Required]
        public string Rol { get; set; }

        public int Id_Detalle { get; set; }

        public bool Es_Externo { get; set; }

        public virtual DetalleUsuario DetalleUsuario { get; set; }
    }
}
