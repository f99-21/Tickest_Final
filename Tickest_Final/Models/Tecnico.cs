using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Tecnico
    {
        public int Id_Tecnico { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Especialidad { get; set; }

        [EmailAddress]
        public string Correo { get; set; }

        public string Telefono { get; set; }

        public int Id_Ticket { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
