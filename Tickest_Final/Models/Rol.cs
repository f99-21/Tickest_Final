using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Rol
    {
        public int Id_Rol { get; set; }

        [Required]
        public string Rol_Nombre { get; set; }

        public string Descripcion { get; set; }
    }
}
