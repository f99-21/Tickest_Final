using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Categoria
    {
        public int Id_Categoria { get; set; }

        [Required]
        public string Categoria_Nombre { get; set; }
    }
}
