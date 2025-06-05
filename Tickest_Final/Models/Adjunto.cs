using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Adjunto
    {
        public int Id_Adjunto { get; set; }

        public int Id_Ticket { get; set; }

        [Required]
        public string Adjunto_Filename { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
