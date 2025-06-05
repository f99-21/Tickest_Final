using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Ticket
    {
        public int Id_Ticket { get; set; }

        [Required]
        public string Asunto { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public DateTime Fecha_Creacion { get; set; }

        public int Id_Categoria { get; set; }

        [Required]
        public string Prioridad { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        public string Nombre_Cliente { get; set; }

        [Required]
        [EmailAddress]
        public string Correo_Cliente { get; set; }

        [Required]
        public string Telefono_Cliente { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
