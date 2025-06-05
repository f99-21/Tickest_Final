using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Permiso
    {
        public int Id_Permiso { get; set; }

        [Required]
        public string Permiso_Nombre { get; set; }

        public string Descripcion { get; set; }
    }
}
