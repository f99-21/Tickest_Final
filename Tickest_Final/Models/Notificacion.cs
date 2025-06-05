using System;

namespace ProyectoFinal.Models
{
    public class Notificacion
    {
        public int Id_Notificacion { get; set; }

        public int Id_Ticket { get; set; }

        public int Id_Usuario { get; set; }

        public string Mensaje { get; set; }

        public DateTime Fecha_Envio { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
