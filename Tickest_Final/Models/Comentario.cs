using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Comentario
    {
        public int Id_Comentario { get; set; }

        public int Id_Ticket { get; set; }

        public int Id_Tecnico { get; set; }

        [Required]
        public string Comentario_Texto { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual Tecnico Tecnico { get; set; }
    }
}
